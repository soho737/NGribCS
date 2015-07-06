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


            coreTest(g2m);
        
        }


        private void coreTest(Grib2Manager g2m, int pNum=306)
        {
            List<InventoryItem> iv = g2m.GetInventory();

            int rn = 0;
            foreach (InventoryItem ivi in iv)
            {

                Console.WriteLine(rn + " / " + ivi.Product.Discipline.DisciplineId + " - " + ivi.Product.ParameterCategory.Id.ToString() + " - " + ivi.Product.Parameter.Id.ToString() + " - " + ivi.Product.Parameter.Abbreviation);
                rn++;
            }


            IGrib2Product pro = g2m.GetProduct(iv[pNum]);
            IGrib2Record rec = g2m.GetRecord(iv[pNum]);



            Grib2GridDefinitionSection gds = g2m.GetGDS(iv[pNum]);

            float[,] fx = g2m.GetGriddedData(iv[pNum]);
            PointF[,] cg = g2m.GetCoordinateGrid(iv[pNum]);

            float min = fx.Min();
            float max = fx.Max();

            float delta = max - min;
            float step = 255 / delta;



            System.Drawing.Bitmap bmp = new Bitmap(gds.Nx, gds.Ny);

            for (int x = 0; x < gds.Nx; x++)
                for (int y = 0; y < gds.Ny; y++)
                {
                    float check = fx[x, y];
                    Color c = Color.FromArgb(255, (int)(255-step*(max-check)), (int)(255-step* (max - check)));
                    bmp.SetPixel(x, y,c);
                }
            /////////////

            pictureBox1.Image = bmp;

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            Grib2Manager g2m = new Grib2Manager(true);
           // 

            g2m.AddFile(@"e:\gfs.t12z.pgrb2.0p25.f000");


            coreTest(g2m);
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Grib2Manager g2m = new Grib2Manager(true);
            g2m.AddFile(@"e:\gribdata.grb2");


            coreTest(g2m, 47);
        }
    }
}
