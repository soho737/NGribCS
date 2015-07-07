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
                treeView2.Nodes.Clear();

                Grib2ProductId pid = treeView1.SelectedNode.Tag as Grib2ProductId;
                List<InventoryItem> ivs = g2m.Inventory.GetAllRecordsForProduct(pid.Discipline.DisciplineId, pid.Category.Id, pid.Parameter.Id);

                StringBuilder info = new StringBuilder();
                info.Append(pid.Parameter.ToString() + Environment.NewLine + "Found " + ivs.Count.ToString() + " record(s).");

                IEnumerable<DateTime> vTimes = g2m.Inventory.GetAllValidTimesForProduct(pid.Discipline.DisciplineId, pid.Category.Id, pid.Parameter.Id);
                info.Append(Environment.NewLine + "Valid time(s):");

                foreach (DateTime dt in vTimes)
                    info.Append(Environment.NewLine + dt.ToString());


                IEnumerable<Grib2SurfaceDefinition> Surfaces = g2m.Inventory.GetAllSurfacesForProduct(pid.Discipline.DisciplineId, pid.Category.Id, pid.Parameter.Id);
                foreach (Grib2SurfaceDefinition gs in Surfaces)
                {
                    info.Append(Environment.NewLine + gs.ToString());

                    if (!treeView2.Nodes.ContainsKey(gs.ToString())) ;
                         treeView2.Nodes.Add(gs.ToString(),gs.ToString());

                     List<InventoryItem> records = g2m.Inventory.GetAllValidTimesForProductAndSurface(pid.Discipline.DisciplineId, pid.Category.Id, pid.Parameter.Id, gs);
                    foreach (InventoryItem record in records)
                    {
                        TreeNode tn = new TreeNode(record.Product.ValidTime.ToString());
                        tn.Tag = record;
                        treeView2.Nodes[gs.ToString()].Nodes.Add(tn);
                    }
                }

                treeView2.ExpandAll();
                textBox1.Text = info.ToString();

            }
            else
                 textBox1.Text = "No parameter node selected";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Plotting
            if (treeView2.SelectedNode != null && treeView2.SelectedNode.Tag != null && treeView2.SelectedNode.Tag is InventoryItem)
            {
                InventoryItem target = treeView2.SelectedNode.Tag as InventoryItem;


                Grib2GridDefinitionSection gds = g2m.GetGDS(target);

                float[,] fx = g2m.GetGriddedData(target);
                //PointF[,] cg = g2m.GetCoordinateGrid(target);

               
                float min = fx.Min();
                float max = fx.Max();

                float delta = max - min;
                float step = 255 / delta;



                System.Drawing.Bitmap bmp = new Bitmap(gds.Nx, gds.Ny);

                for (int x = 0; x < gds.Nx; x++)
                    for (int y = 0; y < gds.Ny; y++)
                    {
                        float check = fx[x, y];
                        Color c = GetColor(min, max, check);
                        bmp.SetPixel(x, y, c);
                    }
         

                pictureBox1.Image = bmp;
            }
        }


        Color GetColor(float rangeStart /*Complete Blue*/, float rangeEnd /*Complete Red*/, float actualValue)
        {
            if (rangeStart >= rangeEnd) return Color.Magenta;

            float max = rangeEnd - rangeStart; // make the scale start from 0
            float value = actualValue - rangeStart; // adjust the value accordingly

            float red = (255 * value) / max; // calculate green (the closer the value is to max, the greener it gets)
            float blue = 255 - red; // set red as inverse of green

            return Color.FromArgb(255, (byte)red, 0, (byte)blue);
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }


      
    }
}
