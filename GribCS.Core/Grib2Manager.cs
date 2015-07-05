using NGribCS.Grib2;
using System;
using System.Collections;
using System.Collections.Generic;
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

      

        /// <summary>
        /// Initializes a new instance of the Grib2Manager from a grib file
        /// </summary>
        /// <param name="pInMemoryProcessing">Process the grib2-File in memory. This is significantly faster, but consumes more ressources</param>
        public Grib2Manager(bool pInMemoryProcessing)
        {
            _inMemoryProcessing = pInMemoryProcessing;
            _StreamDictionary = new Dictionary<int, Stream>();
            _Grib2Inputs = new Dictionary<int, Grib2Input>();
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

                
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
            finally
            {

            }
        }


        public List<InventoryItem> GetInventory()
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
            
            // This byte is reversed due to endianess, so the bit with the index 7 is actually bit 1
            if (ba[7])
            {
                // Points of first row or column scan in the +i direction
            }
            else
            {
                // Points of first row or column scan in the -i direction
            }

            if (ba[6])
            {
                // Points of first row or column scan in the -j direction
            }
            else
            {
                //	Points of first row or column scan in the +j direction
            }

            if (ba[5])
            {
            	// Adjacent points in i direction are consecutive
            }
            else
            {
               // Adjacent points in j direction are consecutive
            }

            if (ba[4])
            {
               // All rows scan in the same direction
            }
            else
            {
               // Adjacent rows scan in the opposite direction
            }

            
            // Defining the array bounds
            float[,] fx = new float[gds.Nx, gds.Ny];

            int n = 0;
            // Currently we are assuming we scan in +i and +j, we'll fix this later
            for (int i = 0; i < gds.Nx;i++ )
            {
                for (int j = 0; j < gds.Ny; j++)
                {
                    fx[i, j] = rawdata[n];
                    n++;
                }

            }

            return fx;

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
