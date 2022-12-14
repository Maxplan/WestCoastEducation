using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WestCoastEdu.BCL
{
    public class Administrator : Account
    {
        public Account Find()
        {
            return new Administrator();
        }

        public void RemoveAccount(int index){ // Remove Account from database
        }
        public Student CreateAccount(string ssn){ // Can create new accounts
            var newStudent = new Student("ssn", "ssn", "ssn");
            return newStudent;
        }
    }
}