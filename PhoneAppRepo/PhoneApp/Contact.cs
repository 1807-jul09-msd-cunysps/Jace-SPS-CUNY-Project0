using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp
{
	public class Contact
	{
		string fName, lName, gender;
        int contactId, age, addressID;
        long phoneNumber;

        Address address;

        public string FName { get => fName; set => fName = value; }
        public string LName { get => lName; set => lName = value; }
        public string Gender { get => gender; set => gender = value; }
        public int AddressID { get => addressID; set => addressID = value; }
        public int ContactId { get => contactId; set => contactId = value; }
        public int Age { get => age; set => age = value; }
        public long PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        internal Address Address { get => address; set => address = value; }

        public Contact() { }
        public Contact(Address a)
        {
            this.address = a;
        }

        public Contact(string fName, string lName, int age, string gender, int addressID, long phoneNumber, Address a)
        {
            this.fName = fName;
            this.lName = lName;
            this.gender = gender;
            this.addressID = addressID;
            this.age = age;
            this.phoneNumber = phoneNumber;
            this.Address = a;
        }

        public string displayContact()
		{
            string str = "";
            str += $"Contact ID: {this.ContactId} \n";
            str += $"First Name: {this.FName} \n";
            str += $"Last Name: {this.LName} \n";
            str += $"Gender: {this.Gender} \n";
            str += $"Age: {this.Age} \n";
            str += $"Phone Number: {this.PhoneNumber} \n";
            foreach (var item in AddressDA.AddList)
            {
                if(item.AddressID == this.AddressID)
                {
                    str += $"Address: {item.AddressST} \n";
                    str += $"City: {item.City} \n";
                    str += $"State: {item.State} \n";
                    str += $"Zip Code: {item.ZipCode} \n";
                    foreach (var element in CountryDA.CtryList)
                    {
                        if(element.CountryID == item.CountryID)
                        {
                            str += $"Country: {element.CountryName} \n";
                            str += $"Country Code: {element.CountryCode} \n";
                        }
                    }
                }
            }

            return str;
		}
	}
}
