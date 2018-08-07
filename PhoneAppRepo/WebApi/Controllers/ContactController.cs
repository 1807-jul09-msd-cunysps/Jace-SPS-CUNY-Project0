using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using PhoneApp;

namespace WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ContactController : ApiController
    {
        [HttpGet] //View or Read Database
        public IHttpActionResult Get()
        {
            try
            {
                var contacts = ContactDA.read();
                return Json(contacts);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }
        [HttpPost] //Insert to Database
        public IHttpActionResult Post([FromBody]Contact postContact)
        {
            if (postContact != null)
            {
                Country country = null;
                bool storeData = true;
                foreach (var item in CountryDA.CtryList)
                {
                    if (Convert.ToString(item.CountryName).ToLower() == postContact.Address.Country.CountryName.ToLower())
                    {
                        country = item;
                        storeData = false;
                    }
                }
                if (storeData)
                {
                    country = new Country();
                    country.CountryName = postContact.Address.Country.CountryName;
                    country.CountryCode = postContact.Address.Country.CountryCode;
                    //===============================================
                    //DATA ACCESS METHOD
                    CountryDA.insert(country);
                    //===============================================
                }
                Address a = new Address(postContact.Address.AddressST, postContact.Address.City, postContact.Address.State, postContact.Address.ZipCode,country.CountryID, country);
                AddressDA.insert(a);
                Contact con = new Contact(
                    postContact.FName, 
                    postContact.LName, 
                    postContact.Age, 
                    postContact.Gender,
                    a.AddressID,
                    postContact.PhoneNumber,
                    a);
                ContactDA.insert(con);

                return Ok("Contact Added.");
            }
            else { return BadRequest(); }
        }
        [HttpPut]
        public IHttpActionResult Put([FromBody]Contact putContact)
        {
            if(putContact != null)
            {
                int[] updateIntArrayValue =
                {
                putContact.Age,
                putContact.Address.ZipCode,
                putContact.Address.Country.CountryCode
            };
                string[] updateIntArrayCol =
                {
                "AGE",
                "ZIP_CODE",
                "COUNTRY_CODE"
            };

                string[] updateStringArrayCol = {
                "FIRST_NAME",
                "LAST_NAME",
                "GENDER",
                "ADDRESS",
                "CITY",
                "STATE",
                "COUNTRY_NAME"
            };
                string[] updateStringArrayValue = {
                putContact.FName,
                putContact.LName,
                putContact.Gender,
                putContact.Address.AddressST,
                putContact.Address.City,
                putContact.Address.State,
                putContact.Address.Country.CountryName
            };

                for (int i = 0; i < updateIntArrayCol.Length; i++)
                {
                    ContactDA.update(updateIntArrayCol[i], putContact, Convert.ToString(updateIntArrayValue[i]));
                }
                for (int i = 0; i < updateStringArrayCol.Length; i++)
                {
                    ContactDA.update(updateStringArrayCol[i], putContact, updateStringArrayValue[i]);
                }
                ContactDA.update("PHONE_NUMBER", putContact, Convert.ToString(putContact.PhoneNumber));

                return Ok("Contact Updated.");
            }
            else { return BadRequest(); }
        }
    }
}
