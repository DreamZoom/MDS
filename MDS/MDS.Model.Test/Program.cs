using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Dynamic;

namespace MDS.Model.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            test t = new test();
            t.Name = "aa";

            Stopwatch watch1 = Stopwatch.StartNew();
            for (var i = 1; i < 100000000; i++)
            {
          
               t.getValue("Name");
               var a = t.Name;
            }
            watch1.Stop();

            Console.WriteLine(watch1.Elapsed.ToString());
            Console.WriteLine(t.getValue("Name"));
            Console.ReadKey();

        }

    }


    public class test : Model
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public object getValue(string pName)
        {
            //if (pName == "Name") return this.Name;
            //if (pName == "Age") return this.Age;
            switch (pName)
            {
                case "Name": return this.Name;
                case "Age": return this.Age;
            }
            return null;
        }
    }
}
