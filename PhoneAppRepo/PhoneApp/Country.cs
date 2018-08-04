using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp
{
    public class Country
    {
        int countryID, countryCode;
        string countryName;

        public int CountryID { get => countryID; set => countryID = value; }
        public int CountryCode { get => countryCode; set => countryCode = value; }
        public string CountryName { get => countryName; set => countryName = value; }
    }
}
