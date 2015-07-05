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
            Console.WriteLine("Start");
            Console.BufferHeight = 500;

            DateTime start = DateTime.Now;
         
            ///////////// WORK HERE

            Grib2Manager g2m = new Grib2Manager(false);
           // g2m.AddFile(@"e:\gribdata.grb2");

            g2m.AddFile(@"e:\gfs.t12z.pgrb2.1p00.f000");
            List<InventoryItem> iv =  g2m.GetInventory();

            int rn = 0;
            foreach (InventoryItem ivi in iv)
            {
               
                Console.WriteLine(rn + " / " +  ivi.Product.Discipline.DisciplineId + " - " + ivi.Product.ParameterCategory.Id.ToString() + " - " + ivi.Product.Parameter.Id.ToString() + " - " + ivi.Product.Parameter.Abbreviation);
                rn++;
            }


            IGrib2Product pro = g2m.GetProduct(iv[306]);
            IGrib2Record rec = g2m.GetRecord(iv[306]);

          //  float[] data = g2m.GetRawData(iv[306]);

            Grib2GridDefinitionSection gds = g2m.GetGDS(iv[306]);


            float[,] fx = g2m.GetGriddedData(iv[306]);


            

           /////////////

            DateTime stop = DateTime.Now;

            TimeSpan ts = stop - start;

            Console.WriteLine("Processing took: " + ts.TotalMilliseconds.ToString() + "ms");

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
