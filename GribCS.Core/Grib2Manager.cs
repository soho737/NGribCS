using NGribCS.Grib2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace NGribCS.grib2
{
    public class Grib2Manager : IDisposable
    {



        private bool _inMemoryProcessing = false;

        private Dictionary<int,Stream> _StreamDictionary;
        private Dictionary<int, Grib2Input> _Grib2Inputs;

        private int sourcecount = 0;

        private Inventory _grib2Inventory;

        public Inventory Inventory
        {
            get
            {
                return _grib2Inventory;
            }

        }

        /// <summary>
        /// Initializes a new instance of the Grib2Manager from a grib file
        /// </summary>
        /// <param name="pInMemoryProcessing">Process the grib2-File in memory. This is significantly faster, but consumes more ressources</param>
        public Grib2Manager(bool pInMemoryProcessing)
        {
            _inMemoryProcessing = pInMemoryProcessing;
            _StreamDictionary = new Dictionary<int, Stream>();
            _Grib2Inputs = new Dictionary<int, Grib2Input>();
            _grib2Inventory = new Inventory(new List<InventoryItem>() { });
        }


        public void AddFile(string pFileName)
        {
            Stream _sourceStream;
            try
            {
                if (_inMemoryProcessing)
                {
                    using (FileStream fs = new System.IO.FileStream(pFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        _sourceStream = new MemoryStream();
                        copyStream(fs, _sourceStream);
                        _sourceStream.Position = 0;
                    }
                }
                else
                {
                    _sourceStream = new System.IO.FileStream(pFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                }

                Grib2Input g2i = new Grib2Input(_sourceStream);
                g2i.scan(true, false);

                _StreamDictionary.Add(sourcecount, _sourceStream);
                _Grib2Inputs.Add(sourcecount, g2i);

                // RebuildInventory
                _grib2Inventory = new Inventory(getInventory());

                sourcecount++;
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
            finally
            {

            }
        }


        private List<InventoryItem> getInventory()
        {
            List<InventoryItem> iv =new List<InventoryItem>();
            foreach (int key in _Grib2Inputs.Keys)
            {
                int i = 0;
                foreach (Grib2Product gp in _Grib2Inputs[key].Products)
                {
                    iv.Add(new InventoryItem(key, i++, gp));
                }

          
            }
            return iv;
        }
    

        public IGrib2Product GetProduct(InventoryItem i)
        {
            return  _Grib2Inputs[i.SourceIndex].GetProduct(i.ProductIndex);
        }

        public float[] GetRawData(InventoryItem i)
        {
            Grib2Data d = new Grib2Data(_StreamDictionary[i.SourceIndex]);
            float[] data = d.getData(i.Product.getGdsOffset(), i.Product.getPdsOffset());
            return data;
        }

        public float[,] GetGriddedData(InventoryItem iv)
        {
           
            Grib2GridDefinitionSection gds = GetGDS(iv);
            float[] rawdata = GetRawData(iv);

            // Determine Scanning Mode
            BitArray ba = new BitArray(new byte[] {(Byte)gds.ScanMode});
            
            // Defining the array bounds
            float[,] fx = new float[gds.Nx, gds.Ny];

            int n = 0;


            // I am certain there is a more elegant way to to this but right now it has 35 degrees, so I am aiming for the simple solution
            if (gds.ScanningDirection == ScanningDirectionMode.AdjacentRowsOppositeDirection)
                throw new NotImplementedException("This scanning mode is not supported.");
            
            if (gds.ScanningConsecutivity == ScanningConsecutivityMode.AdjacentPointsIDirectionConsecutive)
            {
                if (gds.VerticalScanning == VerticalScanningMode.TopToBottom)
                {
                    for (int j = 0; j < gds.Ny; j++)
                    {
                        if (gds.HorizontalScanning == HorizontalScanningMode.LeftToRight)
                        {
                            for (int i = 0; i < gds.Nx; i++)
                            {
                            
                                    fx[i, j] = rawdata[n];
                               

                                n++;
                            }
                        }
                        else if (gds.HorizontalScanning == HorizontalScanningMode.RightToLeft)
                        {
                            for (int i = gds.Nx-1; i >= 0; i--)
                            {
                                fx[i, j] = rawdata[n];
                                n++;
                            }
                        }
                    }
                }
                else if (gds.VerticalScanning == VerticalScanningMode.BottomToTop)
                {
                    for (int j = gds.Ny - 1; j >= 0; j--)
                    {
                        if (gds.HorizontalScanning == HorizontalScanningMode.LeftToRight)
                        {
                            for (int i = 0; i < gds.Nx; i++)
                            {
                                fx[i, j] = rawdata[n];
                                n++;
                            }
                        }
                        else if (gds.HorizontalScanning == HorizontalScanningMode.RightToLeft)
                        {
                            for (int i = gds.Nx - 1; i >= 0; i--)
                            {
                                fx[i, j] = rawdata[n];
                                n++;
                            }
                        }
                    }
                }
            }
            else if (gds.ScanningConsecutivity == ScanningConsecutivityMode.AdjacentPointsJDirectionConsecutive)
            {
                if (gds.HorizontalScanning == HorizontalScanningMode.LeftToRight)
                {
                    for (int i=0; i<gds.Nx; i++)
                    {
                        if (gds.VerticalScanning== VerticalScanningMode.TopToBottom)
                        {
                            for (int j = 0; j < gds.Ny; j++)
                            {
                                fx[i, j] = rawdata[n];
                                n++;
                            }
                        }
                        else if (gds.VerticalScanning == VerticalScanningMode.BottomToTop)
                        {
                            for (int j= gds.Ny -1; j >= 0; j--)
                            {
                                fx[i, j] = rawdata[n];
                                n++;
                            }
                        }
                    }
                }
                else if (gds.HorizontalScanning == HorizontalScanningMode.RightToLeft)
                {
                    for (int i = gds.Nx - 1; i>=0; i--)
                    {
                        if (gds.VerticalScanning == VerticalScanningMode.TopToBottom)
                        {
                            for (int j = 0; j < gds.Ny; j++)
                            {
                                fx[i, j] = rawdata[n];
                                n++;
                            }
                        }
                        else if (gds.VerticalScanning == VerticalScanningMode.BottomToTop)
                        {
                            for (int j = gds.Ny - 1; j >= 0; j--)
                            {
                                fx[i, j] = rawdata[n];
                                n++;
                            }
                        }
                    }
                }
            }


            return fx;

        }

        public PointF[,] GetCoordinateGrid(InventoryItem iv)
        {


            Grib2GridDefinitionSection gds = GetGDS(iv);
            PointF[,] coordinateGrid = new PointF[gds.Nx, gds.Ny];

            
            //if (gds.Gdtn == 0) //LatLon
            //{ 

            // L2R
            // Lo1 = 0
            // Lo2 = 90
            // Dx = 1

            // R2L
            // Lo1 = 90
            // Lo2 = 0
            // Dx = -1 // Ist das dann negativ?


            // x=0
            // SOLL: 0

            // x=1
            // Soll: 1


            for (int x = 0; x < gds.Nx; x++)
                for (int y = 0; y < gds.Ny; y++)
                {
                    float xval = float.NaN;
                    if (gds.HorizontalScanning == HorizontalScanningMode.LeftToRight)
                        xval = gds.Lo1 + gds.Dx * x;
                    else if (gds.HorizontalScanning == HorizontalScanningMode.RightToLeft)
                    {
                        xval = gds.Lo2 + gds.Dx * x;
                    }
                    float yval = float.NaN;
                    if (gds.VerticalScanning == VerticalScanningMode.TopToBottom)
                        yval = gds.La1 - gds.Dy * y;
                    else if (gds.VerticalScanning == VerticalScanningMode.BottomToTop)
                    {
                        yval = gds.La2 + gds.Dy * y;
                    }

                    coordinateGrid[x, y] = new PointF(xval, yval);

                }

            return coordinateGrid;
            //}

            //throw new NotSupportedException("Other templates than Lat/Lon (0) are not supported right now");
        }


        public Grib2GridDefinitionSection GetGDS(InventoryItem i)
        {
            IGrib2Product p = _Grib2Inputs[i.SourceIndex].GetProduct(i.ProductIndex);

            return _Grib2Inputs[i.SourceIndex].GDSs[i.Product.GDSkey];
         
        }
        public IGrib2Record GetRecord(InventoryItem i)
        {
            _StreamDictionary[i.SourceIndex].Position = 0;
            Grib2Input tmp = new Grib2Input(_StreamDictionary[i.SourceIndex]);
            tmp.scan(false, false);

            return tmp.GetRecord(i.ProductIndex);
        }


        public void Dispose()
        {
            foreach (Grib2Input g2i in _Grib2Inputs.Values)
                g2i.closeFile();

            foreach (Stream s in _StreamDictionary.Values)
                if (s != null)
                    s.Dispose();
        }

        private static void copyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }



    }
}
