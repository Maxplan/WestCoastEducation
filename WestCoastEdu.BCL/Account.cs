using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WestCoastEdu.BCL
{
    public abstract class Account
    {
        //Properties
        public int AccountId { get; init; }
        public string? FirstName { get; set;}
        public string? LastName { get; set;}
        public string? FullName => $"{FirstName} {LastName}";
        public string? SocialSecurityNumber { get; init; }
        public AddressInfo? Address { get; }
        public ContactInfo? Contact { get; }
        public string? AccountRole { get; init; }
        //Methods
        public void NewFirstName(string firstName) => this.FirstName = firstName;
        public void NewLastName(string lastName) => this.LastName = lastName;
        public void ListAccountInfo()
        {
            System.Console.WriteLine("--Personal Info--");
            System.Console.WriteLine("Account ID: " + AccountId);
            System.Console.WriteLine("Social Security Number: " + SocialSecurityNumber);
            System.Console.WriteLine("First Name: " + FirstName);
            System.Console.WriteLine("Last Name: " + LastName);
            System.Console.WriteLine("Role: " + AccountRole + "\n");
            Address?.ListCurrentAddress();
        }
    }
}