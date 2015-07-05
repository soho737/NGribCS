using NGribCS.grib2;
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

            Grib2Manager g2m = new Grib2Manager(true);
            g2m.AddFile(@"e:\gribdata.grb2");

            List<InventoryItem> iv =  g2m.GetInventory();
            IGrib2Product pro = g2m.GetProduct(iv[40]);
            float[] data = g2m.GetRawData(iv[40]);

            Grib2GridDefinitionSection gds = g2m.GetGDS(iv[40]);

            /////////////

            DateTime stop = DateTime.Now;

            TimeSpan ts = stop - start;

            Console.WriteLine("Processing took: " + ts.TotalMilliseconds.ToString() + "ms");

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
