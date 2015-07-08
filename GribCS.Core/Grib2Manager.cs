/*
 * This file is part of NGribCS.
 * The project homepage is http://soho737.github.io/NGribCS
 * 
 *  
 * NGribCS is a fork of GribCS found at http://sourceforge.net/projects/gribcs/ 
 * 
 * NGribCS is brought to you by Karsten Kaehler <ngribcs@kkaehler.net>
 * 
 * GribCS was made by
 * Seaware AB, PO Box 1244, SE-131 28, Nacka Strand, Sweden, info@seaware.se.
 * 
 * GribCS itself is based on an automatic conversion of JGRIB Beta 7 
 * (http://jgrib.sourceforge.net/) from Java to C#.
 * 
 * Java-code: Copyright 1997-2006 Unidata Program Center/University 
 * Corporation for Atmospheric Research, P.O. Box 3000, Boulder, CO 80307,
 * support@unidata.ucar.edu.
 * 
 * NGribCS is free software: you can redistribute it and/or modify it under 
 * the terms of the GNU Lesser General Public License as published by the 
 * Free Software Foundation, either version 3 of the License, or (at your 
 * option) any later version.
 * 
 * NGribCS is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of 
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License 
 * along with GribCS.  If not, see <http://www.gnu.org/licenses/>.
*/


using NGribCS.Grib2;
using NGribCS.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace NGribCS.Grib2
{
    /// <summary>
    /// This is the main class for interfacing with the GRIB2 subsystem of NGribCS
    /// </summary>
    public class Grib2Manager : IDisposable
    {

        private bool _inMemoryProcessing = false;

        private Dictionary<int,Stream> _StreamDictionary;
        private Inventory _grib2Inventory;
        private Dictionary<int, Grib2Input> _Grib2Inputs;

        private int sourcecount = 0;

        /// <summary>
        /// This is the global inventory containing all records in all openend grib2-files
        /// </summary>
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
        /// <param gridTemplateName="pInMemoryProcessing">Process the grib2-File in memory. This is significantly faster, but consumes more ressources</param>
        public Grib2Manager(bool pInMemoryProcessing)
        {
            _inMemoryProcessing = pInMemoryProcessing;
            _StreamDictionary = new Dictionary<int, Stream>();
            _Grib2Inputs = new Dictionary<int, Grib2Input>();
            _grib2Inventory = new Inventory(new List<InventoryItem>() { });
        }


        /// <summary>
        /// Loads and inventarizes a grib2-File
        /// </summary>
        /// <param gridTemplateName="pFileName">Name of the file that should be loaded</param>
        public void LoadGrib2File(string pFileName)
        {
            Stream _sourceStream;

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
            _grib2Inventory = new Inventory(inventarize());

            sourcecount++;

        }

        /// <summary>
        /// Builds a list of all records in the grib file
        /// </summary>
        /// <returns>List of InventoryItems</returns>
        protected List<InventoryItem> inventarize()
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
    
   
        /// <summary>
        /// Returns the raw data grid for a given inventory item as a multidimensional float array
        /// Maybe you don't want to use this, unless you need to do something very specific or
        /// you encountered some mishap in NGribCS when using GetGriddedData
        /// </summary>
        /// <param gridTemplateName="pInvItem">The InventoryItem for which the data should be retrieved</param>
        /// <returns>A multidimensional array float[x,y], scanning direction as defined in the GDS</returns>
        public float[] GetRawData(InventoryItem pInvItem)
        {
            Grib2Data d = new Grib2Data(_StreamDictionary[pInvItem.SourceIndex]);
            float[] data = d.getData(pInvItem.Product.getGdsOffset(), pInvItem.Product.getPdsOffset());
           
            return data;
        }


        /// <summary>
        /// Returns a scanning corrected data grid for a given inventory item as a multidimensional float array
        /// </summary>
        /// <param gridTemplateName="pInvItem">The InventoryItem for which the data should be retrieved</param>
        /// <returns>A multidimensional array float[x,y], scanning is always left to right and top to bottom</returns>
        public float[,] GetGriddedData(InventoryItem iv)
        {
           
            Grib2GridDefinitionSection gds = GetGDS(iv);
            float[] rawdata = GetRawData(iv);

            // Determine Scanning Mode
            BitArray ba = new BitArray(new byte[] {(Byte)gds.ScanMode});
            
            // Defining the array bounds
            float[,] fx = new float[gds.Nx, gds.Ny];

            int n = 0;

            if (gds.IoLonMode != InterpretationOfListOfNumbersMode.NoAppendedList)
                throw new GribNotSupportedException("Lists of optional numbers in the GDS are currently not supported");


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

            if (gds.Gdtn == 0)
            {
                PointF[,] coordinateGrid = new PointF[gds.Nx, gds.Ny];

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
            }

            

            throw new NotSupportedException("Other templates than Lat/Lon (0) are not supported right now");
        }

        /// <summary>
        /// Returns the Grid Definition Section (GDS) for a given InventoryItem
        /// </summary>
        /// <param gridTemplateName="pInvItem">The InventoryItem for which the GDS should be retrieved</param>
        /// <returns>Grid Definition Section (GDS) for given InventoryItem</returns>
        public Grib2GridDefinitionSection GetGDS(InventoryItem pInvItem)
        {
            IGrib2Product p = _Grib2Inputs[pInvItem.SourceIndex].GetProduct(pInvItem.ProductIndex);

            return _Grib2Inputs[pInvItem.SourceIndex].GDSs[pInvItem.Product.GDSkey];
         
        }
    

        /// <summary>
        /// Disposal function
        /// </summary>
        public void Dispose()
        {
            foreach (Grib2Input g2i in _Grib2Inputs.Values)
                g2i.closeFile();

            foreach (Stream s in _StreamDictionary.Values)
                if (s != null)
                    s.Dispose();
        }

        /// <summary>
        /// Internal function to copy the file stream to a memory stream if InMemoryProcessing is enabled
        /// </summary>
        /// <param gridTemplateName="input"></param>
        /// <param gridTemplateName="output"></param>
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
