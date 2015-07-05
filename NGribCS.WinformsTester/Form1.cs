using NGribCS.grib2;
using NGribCS.Grib2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NGribCS.WinformsTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
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

            System.Drawing.Bitmap bmp = new Bitmap(gds.Nx, gds.Ny);

            for (int x = 0; x < gds.Nx; x++)
                for (int y = 0; y < gds.Ny; y++)
                {
                    float check = fx[x, y];
                    bmp.SetPixel(x, y, fx[x,y].Equals(0) ? Color.Blue : Color.White);
                }
           /////////////

            pictureBox1.Image = bmp;
        
        
        }
    }
}
