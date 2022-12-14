using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WestCoastEdu.BCL
{
    public class ContactInfo
    {
        //Fields
        public string? EmailAddress { get; private set; }
        public string? PhoneNumber { get; private set; }
        //Constructors
        public ContactInfo(){}

        public void NewEmailAddress(string emailAddress){ // Can be changed separately from Phone Number
            EmailAddress = emailAddress;
        }
        public void NewPhoneNumber(string phoneNumber){ // Can be changed separately from Email Address
            PhoneNumber = phoneNumber;
        }
        public void ListContactInfo(){ // List the properties of contactinfo of an instance of Account.
            System.Console.WriteLine("\n--Contact Info--");
            System.Console.WriteLine("Email: " + EmailAddress);
            System.Console.WriteLine("Phone Number: " + PhoneNumber);
        }
    }
}