using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestCoastEdu.BCL;


namespace WestCoastEdu.BCL;
class Program
{
    static void Main(string[] args)
    {   
        var max = new Student("Max", "Planstedt", "19970403-2177");

        max.ListAccountInfo();

    }
}
