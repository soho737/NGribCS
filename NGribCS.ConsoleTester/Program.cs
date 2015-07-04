using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGribCS.ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
         
            ///////////// WORK HERE

            Grib2Manager g2m = new Grib2Manager(@"e:\gribdata.grb2", true);

           




            /////////////

            DateTime stop = DateTime.Now;

            TimeSpan ts = stop - start;

            Console.WriteLine("Processing took: " + ts.TotalMilliseconds.ToString() + "ms");

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
