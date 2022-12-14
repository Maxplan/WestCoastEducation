using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WestCoastEdu.BCL
{
    public class AddressInfo
    {
        // Fields
        public string? StreetAddress { get; private set; }
        public string? PostalCode { get; private set; }
        public string? City { get; private set; }
        public string? Country { get; private set; }
        // Methods
        public void NewAddressInfo( // Method for new address. When moving, you rarely keep the same postalCode and City, hence why i chose to make a constructor that demands all properties of a new adress
        string streetAddress, 
        string postalCode, 
        string city, 
        string country
        ){
            StreetAddress = streetAddress;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public void ListCurrentAddress(){ // List the AdressInfo of an Account instance
            System.Console.WriteLine("--Address--");
            System.Console.WriteLine("Street Address: " + this.StreetAddress);
            System.Console.WriteLine("Postal Code: " + this.PostalCode);
            System.Console.WriteLine("City: " + this.City);
            System.Console.WriteLine("Country: " + this.Country);
        }
    }
}