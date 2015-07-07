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

        private Grib2Manager g2m;
        
        public Form1()
        {
            InitializeComponent();
            g2m = new Grib2Manager(true);
        }

        private void resetGrib2ManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g2m.Dispose();
            g2m = new Grib2Manager(true);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addNewFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                g2m.AddFile(openFileDialog1.FileName);

            RebuildTree();
        }



        private void RebuildTree()
        {
            List<Grib2ProductId> distProds = g2m.Inventory.GetDisctinctProducts();


            TreeNode root = new TreeNode();
       
           // Dictionary<int, TreeNode> DisciplineNodes = new Dictionary<int, TreeNode>();

            foreach (Grib2ProductId pid in distProds.OrderBy(x=>x.Parameter.Id).OrderBy(x=>x.Category.Id).OrderBy(x=>x.Discipline.DisciplineId))
            {
                if (!root.Nodes.ContainsKey(pid.Discipline.ToString()))
                    root.Nodes.Add(pid.Discipline.ToString(), pid.Discipline.ToString());

                TreeNode displNode = root.Nodes[pid.Discipline.ToString()];

                if (!displNode.Nodes.ContainsKey(pid.Category.ToString()))
                    displNode.Nodes.Add(pid.Category.ToString(), pid.Category.ToString());

                TreeNode catNode = displNode.Nodes[pid.Category.ToString()];

                if (!catNode.Nodes.ContainsKey(pid.Parameter.Abbreviation))
                    catNode.Nodes.Add(pid.Parameter.Abbreviation, pid.Parameter.Name);

                catNode.Nodes[pid.Parameter.Abbreviation].Tag = pid;
            }

            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(root);

            treeView1.ExpandAll();

        }


        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
          
        }




        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is Grib2ProductId)
            {
                Grib2ProductId pid = treeView1.SelectedNode.Tag as Grib2ProductId;
                List<InventoryItem> ivs = g2m.Inventory.GetAllRecordsForProduct(pid.Discipline.DisciplineId, pid.Category.Id, pid.Parameter.Id);

                StringBuilder info = new StringBuilder();
                info.Append(pid.Parameter.ToString() + Environment.NewLine + "Found " + ivs.Count.ToString() + " record(s).");

                IEnumerable<DateTime> vTimes = ivs.Select(x => x.Product.ValidTime).Distinct();
                info.Append(Environment.NewLine + "Valid time(s):");

                foreach (DateTime dt in vTimes)
                    info.Append(Environment.NewLine + dt.ToString());

                textBox1.Text = info.ToString();

            }
            else
                 textBox1.Text = "No parameter node selected";
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
            List<InventoryItem> iv = g2m.Inventory.InventoryItems;

            int rn = 0;
            foreach (InventoryItem ivi in iv)
            {

                Console.WriteLine(rn + " / " + ivi.Product.ProductIdentification.Discipline.DisciplineId + " - " + ivi.Product.ProductIdentification.Category.Id.ToString() + " - " + ivi.Product.ProductIdentification.Parameter.Id.ToString() + " - " + ivi.Product.ProductIdentification.Parameter.Abbreviation);
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

           // pictureBox1.Image = bmp;

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

        private void button4_Click(object sender, EventArgs e)
        {
            Grib2Manager g2m = new Grib2Manager(true);
            g2m.AddFile(@"e:\gribdata.grb2");

            Inventory inv = g2m.Inventory;
            //List<Grib2ProductId> = inv.GetDisctinctProducts();

        }


      
    }
}
