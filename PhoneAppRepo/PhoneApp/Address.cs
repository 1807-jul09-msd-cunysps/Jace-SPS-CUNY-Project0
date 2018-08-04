using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp
{
    public class Address
	{
        int addressID, countryID, zipCode;
        string address, city, state;

        Country country;

        public int AddressID { get => addressID; set => addressID = value; }
        public int CountryID { get => countryID; set => countryID = value; }
        public int ZipCode { get => zipCode; set => zipCode = value; }
        public string AddressST { get => address; set => address = value; }
        public string City { get => city; set => city = value;}
        public string State { get => state; set => state = value; }
        internal Country Country { get => country; set => country = value; }
        
        public Address(string address, string city, string state, int zipCode, int countryID, Country c)
        {
            this.countryID = countryID;
            this.zipCode = zipCode;
            this.address = address;
            this.city = city;
            this.state = state;
            this.country = c;
        }
    }

}
