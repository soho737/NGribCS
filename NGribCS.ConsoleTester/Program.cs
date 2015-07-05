using NGribCS.Grib2;
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

           
            // Lets output the inventory

            int i = 0;
            foreach (Grib2Product p in g2m.G2I.Products)
            {
                i++;
               // if (p.Parameter.Name!="UNDEFINED")
                Console.WriteLine(i + " of " + g2m.G2I.Products.Count()+":");
                Console.WriteLine("{Discipline " + p.Discipline.DisciplineId.ToString() + " - " + p.Discipline.DisciplineName + "}"); 
              Console.WriteLine("[" + p.ParameterCategory.Id.ToString() + " - " + p.ParameterCategory.Name + "] " + p.Parameter.Id +"/" + p.Parameter.Abbreviation + " - " + p.Parameter.Name + " - " + p.Parameter.Unit);


            }



            /////////////

            DateTime stop = DateTime.Now;

            TimeSpan ts = stop - start;

            Console.WriteLine("Processing took: " + ts.TotalMilliseconds.ToString() + "ms");

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
