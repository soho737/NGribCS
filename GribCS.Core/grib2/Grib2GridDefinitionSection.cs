
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

/* Changelog
 * 
 * 07 Aug 2015 - kk - Introduced enumerators for the scanning direction
 * 08 Aug 2015 - kk - Renamed private variables to increased code readibility, other readibility enhancements
 * 08 Aug 2015 - kk - Checksum calculation fixed
 * 08 Aug 2015 - kk - Introduced enumerator for InterpretationOfListOfNumbers
 * 08 Aug 2015 - kk - Throw an exception if list of numbers is used, as this is ignored by the classic gribcs code and thus not supported right now
 * 
 */


using System;
using System.Collections;
using System.Runtime.InteropServices;
using NGribCS.Helpers;

namespace NGribCS.Grib2
{

    public enum HorizontalScanningMode { LeftToRight, RightToLeft};
    public enum VerticalScanningMode { TopToBottom, BottomToTop}
    public enum ScanningConsecutivityMode { AdjacentPointsIDirectionConsecutive, AdjacentPointsJDirectionConsecutive}
    public enum ScanningDirectionMode { AllRowsSameDirection, AdjacentRowsOppositeDirection}
    public enum InterpretationOfListOfNumbersMode { NoAppendedList = 0,  Mode1 = 1, Mode2 = 2, Missing=255}
	/// <summary> A class that represents the grid definition numberOfSection (GDS) of a GRIB product. </summary>
	public class Grib2GridDefinitionSection : NGribCS.Grib2.IGrib2GridDefinitionSection
	{
		/// <summary> sourceOfGridDefinition of grid definition.</summary>
		/// <returns> sourceOfGridDefinition
		/// </returns>
		public int SourceOfGridDefinition
		{
			get
			{
				return sourceOfGridDefinition;
			}
			
		}
		/// <summary> number of data points .</summary>
		/// <returns> numberOfDataPoints
		/// </returns>
		public int NumberOfDataPoints
		{
			get
			{
				return numberOfDataPoints;
			}
			
		}
		/// <summary> optional list of numbers .</summary>
		/// <returns> numOctetsForOptionalListOfNumbers
		/// </returns>
		public int NumOctetsForOptionalListOfNumbers
		{
			get
			{
				return numOctetsForOptionalListOfNumbers;
			}
			
		}
		/// <summary> iterpretation of optional list of numbers .</summary>
		/// <returns> interpretationOfListOfNumbersCode
		/// </returns>
		public int InterpretationOfListOfNumbersCode
		{
			get
			{
				return interpretationOfListOfNumbersCode;
			}
			
		}

        public InterpretationOfListOfNumbersMode IoLonMode
        {
            get
            {
                return (InterpretationOfListOfNumbersMode)this.InterpretationOfListOfNumbersCode;
            }
        }
		/// <summary> Grid Definition Template Number .</summary>
		/// <returns> gridDefinitionTemplateNumber
		/// </returns>
		public int GridDefinitionTemplateNumber
		{
			get
			{
				return gridDefinitionTemplateNumber;
			}
			
		}
		/// <summary> Grid gridTemplateName .</summary>
		/// <returns> gridName
		/// </returns>
		public System.String GridTemplateName
		{
			get
			{
				return gridTemplateName;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> shape as a int
		/// 
		/// </returns>
		public int Shape
		{
			get
			{
				return shape;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> EarthRadius as a float
		/// 
		/// </returns>
		public float EarthRadius
		{
			get
			{
				return earthRadius;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> MajorAxis as a float
		/// 
		/// </returns>
		public float MajorAxis
		{
			get
			{
				return majorAxis;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> MinorAxis as a float
		/// 
		/// </returns>
		public float MinorAxis
		{
			get
			{
				return minorAxis;
			}
			
		}
		/// <summary> Get number of grid columns.
		/// 
		/// </summary>
		/// <returns> number of grid columns
		/// </returns>
		public int Nx
		{
			get
			{
				return nx;
			}
			
		}
		/// <summary> Get number of grid rows.
		/// 
		/// </summary>
		/// <returns> number of grid rows.
		/// </returns>
		public int Ny
		{
			get
			{
				return ny;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> BasicAngleOfLatLon as a int
		/// 
		/// </returns>
		public int BasicAngleOfLatLon
		{
			get
			{
				return basicAngleOfLatLon;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> UnitOfBasicAngle as a int
		/// 
		/// </returns>
		public int UnitOfBasicAngle
		{
			get
			{
				return unitOfBasicAngle;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> La1 as a float
		/// 
		/// </returns>
		public float La1
		{
			get
			{
				return la1;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Lo1 as a float
		/// 
		/// </returns>
		public float Lo1
		{
			get
			{
				return lo1;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Resolution as a int
		/// 
		/// </returns>
		public int Resolution
		{
			get
			{
				return resolution;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> La2 as a float
		/// 
		/// </returns>
		public float La2
		{
			get
			{
				return la2;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Lo2 as a float
		/// 
		/// </returns>
		public float Lo2
		{
			get
			{
				return lo2;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Lad as a float
		/// 
		/// </returns>
		public float Lad
		{
			get
			{
				return lad;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Lov as a float
		/// 
		/// </returns>
		public float Lov
		{
			get
			{
				return lov;
			}
			
		}
		/// <summary> Get x-increment/distance between two grid points.
		/// 
		/// </summary>
		/// <returns> x-increment
		/// </returns>
		public float Dx
		{
			get
			{
				return dx;
			}
			
		}
		/// <summary> Get y-increment/distance between two grid points.
		/// 
		/// </summary>
		/// <returns> y-increment
		/// </returns>
		public float Dy
		{
			get
			{
				return dy;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> ProjectionCenter as a int
		/// 
		/// </returns>
		public int ProjectionCenter
		{
			get
			{
				return projectionCenter;
			}
			
		}
		/// <summary> Get scan mode.
		/// 
		/// </summary>
		/// <returns> scan mode
		/// </returns>
		public int ScanMode
		{
			get
			{
				return scanMode;
			}
			
		}


        public HorizontalScanningMode HorizontalScanning
        {
            get
            {
                BitArray ba = new BitArray(new byte[] { (Byte)this.ScanMode });

                // This byte is reversed due to endianess, so the bit with the index 7 is actually bit 1
                if (ba[7])
                {
                    return HorizontalScanningMode.RightToLeft;
                }
                else
                {
                    return HorizontalScanningMode.LeftToRight;
                }
            }
        }


        public VerticalScanningMode VerticalScanning
        {
            get
            {
                BitArray ba = new BitArray(new byte[] { (Byte)this.ScanMode });

                // This byte is reversed due to endianess, so the bit with the index 7 is actually bit 1
                if (ba[6])
                {
                    return VerticalScanningMode.BottomToTop;
        
                }
                else
                {
                    return VerticalScanningMode.TopToBottom;
                }
            }
        }

        public ScanningConsecutivityMode ScanningConsecutivity
        {
            get
            {
                BitArray ba = new BitArray(new byte[] { (Byte)this.ScanMode });

                if (ba[5])
                {
                    return ScanningConsecutivityMode.AdjacentPointsJDirectionConsecutive;   
                }
                else
                {
                    return ScanningConsecutivityMode.AdjacentPointsIDirectionConsecutive;
                }
            }
        }

      public ScanningDirectionMode ScanningDirection
        {
          get
            {
                BitArray ba = new BitArray(new byte[] { (Byte)this.ScanMode });

                if (ba[4])
                {
                    return ScanningDirectionMode.AdjacentRowsOppositeDirection;
                }
                else
                {
                    return ScanningDirectionMode.AllRowsSameDirection;
                }
            }
        }

		/// <summary> .</summary>
		/// <returns> Latin1 as a float
		/// 
		/// </returns>
		public float Latin1
		{
			get
			{
				return latin1;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Latin2 as a float
		/// 
		/// </returns>
		public float Latin2
		{
			get
			{
				return latin2;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> SpLat as a float
		/// 
		/// </returns>
		public float SpLat
		{
			get
			{
				return spLat;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> SpLon as a float
		/// 
		/// </returns>
		public float SpLon
		{
			get
			{
				return spLon;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Rotationangle as a float
		/// 
		/// </returns>
		public float Rotationangle
		{
			get
			{
				return rotationangle;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> PoleLat as a float
		/// 
		/// </returns>
		public float PoleLat
		{
			get
			{
				return poleLat;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> PoleLon as a float
		/// 
		/// </returns>
		public float PoleLon
		{
			get
			{
				return poleLon;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Factor as a float
		/// 
		/// </returns>
		public float Factor
		{
			get
			{
				return factor;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> N as a int
		/// 
		/// </returns>
		public int N
		{
			get
			{
				return n;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> J as a float
		/// 
		/// </returns>
		public float J
		{
			get
			{
				return j;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> K as a float
		/// 
		/// </returns>
		public float K
		{
			get
			{
				return k;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> M as a float
		/// 
		/// </returns>
		public float M
		{
			get
			{
				return m;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Method as a int
		/// 
		/// </returns>
		public int Method
		{
			get
			{
				return method;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Mode as a int
		/// 
		/// </returns>
		public int Mode
		{
			get
			{
				return mode;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Lap as a float
		/// 
		/// </returns>
		public float Lap
		{
			get
			{
				return lap;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Lop as a float
		/// 
		/// </returns>
		public float Lop
		{
			get
			{
				return lop;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Xp as a float
		/// 
		/// </returns>
		public float Xp
		{
			get
			{
				return xp;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Yp as a float
		/// 
		/// </returns>
		public float Yp
		{
			get
			{
				return yp;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Xo as a float
		/// 
		/// </returns>
		public float Xo
		{
			get
			{
				return xo;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Yo as a float
		/// 
		/// </returns>
		public float Yo
		{
			get
			{
				return yo;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Altitude as a float
		/// 
		/// </returns>
		public float Altitude
		{
			get
			{
				return altitude;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> N2 as a int
		/// 
		/// </returns>
		public int N2
		{
			get
			{
				return n2;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> N3 as a int
		/// 
		/// </returns>
		public int N3
		{
			get
			{
				return n3;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Ni as a int
		/// 
		/// </returns>
		public int Ni
		{
			get
			{
				return ni;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Nd as a int
		/// 
		/// </returns>
		public int Nd
		{
			get
			{
				return nd;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Position as a int
		/// 
		/// </returns>
		public int Position
		{
			get
			{
				return position;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Order as a int
		/// 
		/// </returns>
		public int Order
		{
			get
			{
				return order;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Nb as a float
		/// 
		/// </returns>
		public float Nb
		{
			get
			{
				return nb;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Nr as a float
		/// 
		/// </returns>
		public float Nr
		{
			get
			{
				return nr;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> Dstart as a float
		/// 
		/// </returns>
		public float Dstart
		{
			get
			{
				return dstart;
			}
			
		}
		/// <summary> .</summary>
		/// <returns> CheckSum as a String
		/// 
		/// </returns>
		public System.String CheckSum
		{
			get
			{
				return checksum;
			}
			
		}
		
        /// <summary>  scale factor for Lat/Lon variables in degrees.</summary>
		private static float tenToNegSix = (float) SupportClass.Identity((1 / 1000000.0));
		private static float tenToNegThree = (float) SupportClass.Identity((1 / 1000.0));
		
		/// <summary> Length in bytes of this numberOfSection.</summary>
		private int lengthOfSection;
		
		/// <summary> numberOfSection number should be 3.</summary>
		private int numberOfSection;
		
		/// <summary> sourceOfGridDefinition of grid definition.</summary>
		private int sourceOfGridDefinition;
		
		/// <summary> number of data points.</summary>
		private int numberOfDataPoints;
		
		/// <summary> optional list of numbers.</summary>
		private int numOctetsForOptionalListOfNumbers;
		
		/// <summary> iterpretation of optional list of numbers.</summary>
		private int interpretationOfListOfNumbersCode;
		
		/// <summary> Grid Definition Template Number.</summary>
		private int gridDefinitionTemplateNumber;
		
		/// <summary> Grid gridTemplateName.</summary>
		private System.String gridTemplateName;
		
		/// <summary> grid definitions from template 3.</summary>
		private int shape;
		private float earthRadius;
		private float majorAxis;
		private float minorAxis;
		
		/// <summary> Number of grid columns. (Also Ni).</summary>
		private int nx;
		
		/// <summary> Number of grid rows. (Also Nj).</summary>
		private int ny;
		
		private int basicAngleOfLatLon;
		private int unitOfBasicAngle;
		private float la1;
		private float lo1;
		private int resolution;
		private float la2;
		private float lo2;
		private float lad;
		private float lov;
		
        /// <summary> x-distance between two grid points - can be delta-Lon or delta x.</summary>
		private float dx;
		
		/// <summary> y-distance of two grid points - can be delta-Lat or delta y.</summary>
		private float dy;
		
		private int projectionCenter;
		private int scanMode;
		private float latin1;
		private float latin2;
		private float spLat;
		private float spLon;
		private float rotationangle;
		private float poleLat;
		private float poleLon;
		private int lonofcenter;
		private int factor;
		private int n;
		private float j;
		private float k;
		private float m;
		private int method;
		private int mode;
		private float xp;
		private float yp;
		private int lap;
		private int lop;
		private int xo;
		private int yo;
		private int altitude;
		private int n2;
		private int n3;
		private int ni;
		private int nd;
		private int position;
		private int order;
		private float nb;
		private float nr;
		private float dstart;
		
		private System.String checksum = "";
		// *** constructors *******************************************************
		
		/// <summary> Constructs a <tt>Grib2GridDefinitionSection</tt> object from a gribStream.
		/// 
		/// </summary>
		/// <param gridTemplateName="gribStream">RandomAccessFile
		/// </param>
		/// <param gridTemplateName="doCheckSum"> calculate the checksum
		/// </param>
		/// <throws>  IOException  if gribStream contains no valid GRIB product </throws>
		public Grib2GridDefinitionSection(System.IO.Stream gribStream, bool doCheckSum)
		{
			int scaleFactorRadius = 0;
			int scaledValueRadius = 0;
			int scaleFactorMajor = 0;
			int scaledValueMajor = 0;
			int scaleFactorMinor = 0;
			int scaledValueMinor = 0;
			
			// octets 1-4 (Length of GDS)
			lengthOfSection = GribNumbers.int4(gribStream);
			
            if (doCheckSum)
            {
                
                // get byte array for this gds, then reset gribStream to same position
                // calculate checksum for this gds via the byte array
                long mark = gribStream.Position;
                sbyte[] dst = new sbyte[lengthOfSection - 4];
                SupportClass.ReadInput(gribStream, dst, 0, dst.Length);
                gribStream.Seek(mark, System.IO.SeekOrigin.Begin);
               
                CRC32 crc32Util = new CRC32();
                byte[] unsigned = new byte[dst.Length];
                Buffer.BlockCopy(dst,0, unsigned, 0, dst.Length);
                checksum = System.Convert.ToBase64String(crc32Util.ComputeHash(unsigned));
            }
			
			numberOfSection = gribStream.ReadByte(); // This is numberOfSection 3
			
            // SourceOfGridDefinition of Grid Definition (Code Table 3.0 and Note 1)
			sourceOfGridDefinition = gribStream.ReadByte();
			
			numberOfDataPoints = GribNumbers.int4(gribStream);
			
			numOctetsForOptionalListOfNumbers = gribStream.ReadByte();
            
            // We need to stop here if this value is other than 0 as this is not respected further downstream
            // At octet 15 the Grid Definition Template starts until octet xx
            // and at octet xx+1 until - nn (numOctetsForOptionalListOfNumbers) should be respected
            if (numOctetsForOptionalListOfNumbers != 0)
                throw new GribNotSupportedException("numOctetsForOptionalNumbers > 0 is not supported");



            // See code Table 3.11
			interpretationOfListOfNumbersCode = gribStream.ReadByte();
			
			gridDefinitionTemplateNumber = GribNumbers.int2(gribStream);
			
			gridTemplateName = getGridName(gridDefinitionTemplateNumber);
			
			float ratio;
			
			switch (gridDefinitionTemplateNumber)
			{
				
				// Grid Definition Template Number
                case 0: // Latitude/Longitud
                case 1: // Rotated Latitude/Longitud
                case 2: // Stretched Latitude/Longitud
				case 3:  // Rotated and Stretched Latitude/Longitude Grid
					shape = gribStream.ReadByte();
					scaleFactorRadius = gribStream.ReadByte();
					scaledValueRadius = GribNumbers.int4(gribStream);
					scaleFactorMajor = gribStream.ReadByte();
					scaledValueMajor = GribNumbers.int4(gribStream);
					scaleFactorMinor = gribStream.ReadByte();
					scaledValueMinor = GribNumbers.int4(gribStream);
					nx = GribNumbers.int4(gribStream);
					ny = GribNumbers.int4(gribStream);
					basicAngleOfLatLon = GribNumbers.int4(gribStream);
					unitOfBasicAngle = GribNumbers.int4(gribStream);
					if (basicAngleOfLatLon == 0)
					{
						ratio = tenToNegSix;
					}
					else
					{
						ratio = basicAngleOfLatLon / unitOfBasicAngle;
					}
					la1 = (float) (GribNumbers.int4(gribStream) * ratio);
					lo1 = (float) (GribNumbers.int4(gribStream) * ratio);
					resolution = gribStream.ReadByte();
					la2 = (float) (GribNumbers.int4(gribStream) * ratio);
					lo2 = (float) (GribNumbers.int4(gribStream) * ratio);
					dx = (float) (GribNumbers.int4(gribStream) * ratio);
					dy = (float) (GribNumbers.int4(gribStream) * ratio);
					scanMode = gribStream.ReadByte();
					
					//  1, 2, and 3 needs checked
					if (gridDefinitionTemplateNumber == 1)
					{
						//Rotated Latitude/longitude
						spLat = GribNumbers.int4(gribStream) * tenToNegSix;
						spLon = GribNumbers.int4(gribStream) * tenToNegSix;
                        rotationangle = GribNumbers.IEEEfloat4(gribStream);
					}
					else if (gridDefinitionTemplateNumber == 2)
					{
						//Stretched Latitude/longitude
						poleLat = GribNumbers.int4(gribStream) * tenToNegSix;
						poleLon = GribNumbers.int4(gribStream) * tenToNegSix;
						factor = GribNumbers.int4(gribStream);
					}
					else if (gridDefinitionTemplateNumber == 3)
					{
						//Stretched and Rotated 
						// Latitude/longitude
						spLat = GribNumbers.int4(gribStream) * tenToNegSix;
						spLon = GribNumbers.int4(gribStream) * tenToNegSix;
                        rotationangle = GribNumbers.IEEEfloat4(gribStream);
						poleLat = GribNumbers.int4(gribStream) * tenToNegSix;
						poleLon = GribNumbers.int4(gribStream) * tenToNegSix;
						factor = GribNumbers.int4(gribStream);
					}
					break;
				
				
				case 10:  // Mercator
					// la1, lo1, lad, la2, and lo2 need checked
					shape = gribStream.ReadByte();
					scaleFactorRadius = gribStream.ReadByte();
					scaledValueRadius = GribNumbers.int4(gribStream);
					scaleFactorMajor = gribStream.ReadByte();
					scaledValueMajor = GribNumbers.int4(gribStream);
					scaleFactorMinor = gribStream.ReadByte();
					scaledValueMinor = GribNumbers.int4(gribStream);
					nx = GribNumbers.int4(gribStream);
					ny = GribNumbers.int4(gribStream);
					la1 = GribNumbers.int4(gribStream) * tenToNegSix;
					lo1 = GribNumbers.int4(gribStream) * tenToNegSix;
					resolution = gribStream.ReadByte();
					lad = GribNumbers.int4(gribStream) * tenToNegSix;
					la2 = GribNumbers.int4(gribStream) * tenToNegSix;
					lo2 = GribNumbers.int4(gribStream) * tenToNegSix;
					scanMode = gribStream.ReadByte();
					basicAngleOfLatLon = GribNumbers.int4(gribStream);
					dx = (float) (GribNumbers.int4(gribStream) * tenToNegThree);
					dy = (float) (GribNumbers.int4(gribStream) * tenToNegThree);
					
					break;
				
				
				case 20:  // Polar stereographic projection
					// la1, lo1, lad, and lov need checked
					shape = gribStream.ReadByte();
					scaleFactorRadius = gribStream.ReadByte();
					scaledValueRadius = GribNumbers.int4(gribStream);
					scaleFactorMajor = gribStream.ReadByte();
					scaledValueMajor = GribNumbers.int4(gribStream);
					scaleFactorMinor = gribStream.ReadByte();
					scaledValueMinor = GribNumbers.int4(gribStream);
					nx = GribNumbers.int4(gribStream);
					ny = GribNumbers.int4(gribStream);
					la1 = GribNumbers.int4(gribStream) * tenToNegSix;
					lo1 = GribNumbers.int4(gribStream) * tenToNegSix;
					resolution = gribStream.ReadByte();
					lad = GribNumbers.int4(gribStream) * tenToNegSix;
					lov = GribNumbers.int4(gribStream) * tenToNegSix;
					dx = (float) (GribNumbers.int4(gribStream) * tenToNegThree);
					dy = (float) (GribNumbers.int4(gribStream) * tenToNegThree);
					projectionCenter = gribStream.ReadByte();
					scanMode = gribStream.ReadByte();
					
					break;
				
				
				case 30:  // Lambert Conformal
					shape = gribStream.ReadByte();
					scaleFactorRadius = gribStream.ReadByte();
					scaledValueRadius = GribNumbers.int4(gribStream);
					scaleFactorMajor = gribStream.ReadByte();
					scaledValueMajor = GribNumbers.int4(gribStream);
					scaleFactorMinor = gribStream.ReadByte();
					scaledValueMinor = GribNumbers.int4(gribStream);
					nx = GribNumbers.int4(gribStream);
					ny = GribNumbers.int4(gribStream);
					la1 = (float) (GribNumbers.int4(gribStream) * tenToNegSix);
					lo1 = (float) (GribNumbers.int4(gribStream) * tenToNegSix);
					resolution = gribStream.ReadByte();
					lad = (float) (GribNumbers.int4(gribStream) * tenToNegSix);
					lov = (float) (GribNumbers.int4(gribStream) * tenToNegSix);
					dx = (float) (GribNumbers.int4(gribStream) * tenToNegThree);
					dy = (float) (GribNumbers.int4(gribStream) * tenToNegThree);
					projectionCenter = gribStream.ReadByte();
					scanMode = gribStream.ReadByte();
					latin1 = (float) (GribNumbers.int4(gribStream) * tenToNegSix);
					latin2 = (float) (GribNumbers.int4(gribStream) * tenToNegSix);
					spLat = (float) (GribNumbers.int4(gribStream) * tenToNegSix);
					spLon = (float) (GribNumbers.int4(gribStream) * tenToNegSix);
					
					break;
				
				
				case 31:  // Albers Equal Area
					// la1, lo1, lad, and lov need checked
					shape = gribStream.ReadByte();
					scaleFactorRadius = gribStream.ReadByte();
					scaledValueRadius = GribNumbers.int4(gribStream);
					scaleFactorMajor = gribStream.ReadByte();
					scaledValueMajor = GribNumbers.int4(gribStream);
					scaleFactorMinor = gribStream.ReadByte();
					scaledValueMinor = GribNumbers.int4(gribStream);
					nx = GribNumbers.int4(gribStream);
					ny = GribNumbers.int4(gribStream);
					la1 = GribNumbers.int4(gribStream) * tenToNegSix;
					lo1 = GribNumbers.int4(gribStream) * tenToNegSix;
					resolution = gribStream.ReadByte();
					lad = GribNumbers.int4(gribStream) * tenToNegSix;
					lov = GribNumbers.int4(gribStream) * tenToNegSix;
					dx = (float) (GribNumbers.int4(gribStream) * tenToNegThree);
					dy = (float) (GribNumbers.int4(gribStream) * tenToNegThree);
					projectionCenter = gribStream.ReadByte();
					scanMode = gribStream.ReadByte();
					latin1 = GribNumbers.int4(gribStream) * tenToNegSix;
					latin2 = GribNumbers.int4(gribStream) * tenToNegSix;
					spLat = GribNumbers.int4(gribStream) * tenToNegSix;
					spLon = GribNumbers.int4(gribStream) * tenToNegSix;
					
					break;
				
				
				case 40: 
				case 41: 
				case 42: 
				case 43:  // Gaussian latitude/longitude
					shape = gribStream.ReadByte();
					scaleFactorRadius = gribStream.ReadByte();
					scaledValueRadius = GribNumbers.int4(gribStream);
					scaleFactorMajor = gribStream.ReadByte();
					scaledValueMajor = GribNumbers.int4(gribStream);
					scaleFactorMinor = gribStream.ReadByte();
					scaledValueMinor = GribNumbers.int4(gribStream);
					nx = GribNumbers.int4(gribStream);
					ny = GribNumbers.int4(gribStream);
					basicAngleOfLatLon = GribNumbers.int4(gribStream);
					unitOfBasicAngle = GribNumbers.int4(gribStream);
					if (basicAngleOfLatLon == 0)
					{
						ratio = tenToNegSix;
					}
					else
					{
						ratio = basicAngleOfLatLon / unitOfBasicAngle;
					}
					la1 = (float) (GribNumbers.int4(gribStream) * ratio);
					lo1 = (float) (GribNumbers.int4(gribStream) * ratio);
					resolution = gribStream.ReadByte();
					la2 = (float) (GribNumbers.int4(gribStream) * ratio);
					lo2 = (float) (GribNumbers.int4(gribStream) * ratio);
					dx = (float) (GribNumbers.int4(gribStream) * ratio);
					n = gribStream.ReadByte();
					scanMode = gribStream.ReadByte();
					
					if (gridDefinitionTemplateNumber == 41)
					{
						//Rotated Gaussian Latitude/longitude
						
						spLat = GribNumbers.int4(gribStream) * ratio;
						spLon = GribNumbers.int4(gribStream) * ratio;
                        rotationangle = GribNumbers.IEEEfloat4(gribStream);
					}
					else if (gridDefinitionTemplateNumber == 42)
					{
						//Stretched Gaussian 
						// Latitude/longitude
						
						poleLat = GribNumbers.int4(gribStream) * ratio;
						poleLon = GribNumbers.int4(gribStream) * ratio;
						factor = GribNumbers.int4(gribStream);
					}
					else if (gridDefinitionTemplateNumber == 43)
					{
						//Stretched and Rotated Gaussian  
						// Latitude/longitude
						
						spLat = GribNumbers.int4(gribStream) * ratio;
						spLon = GribNumbers.int4(gribStream) * ratio;
                        rotationangle = GribNumbers.IEEEfloat4(gribStream);
						poleLat = GribNumbers.int4(gribStream) * ratio;
						poleLon = GribNumbers.int4(gribStream) * ratio;
						factor = GribNumbers.int4(gribStream);
					}
					break;
				
				
				case 50: 
				case 51: 
				case 52: 
				case 53:  // Spherical harmonic coefficients

                    j = GribNumbers.IEEEfloat4(gribStream);
                    k = GribNumbers.IEEEfloat4(gribStream);
                    m = GribNumbers.IEEEfloat4(gribStream);
					method = gribStream.ReadByte();
					mode = gribStream.ReadByte();
					if (gridDefinitionTemplateNumber == 51)
					{
						//Rotated Spherical harmonic coefficients
						
						spLat = GribNumbers.int4(gribStream) * tenToNegSix;
						spLon = GribNumbers.int4(gribStream) * tenToNegSix;
                        rotationangle = GribNumbers.IEEEfloat4(gribStream);
					}
					else if (gridDefinitionTemplateNumber == 52)
					{
						//Stretched Spherical 
						// harmonic coefficients
						
						poleLat = GribNumbers.int4(gribStream) * tenToNegSix;
						poleLon = GribNumbers.int4(gribStream) * tenToNegSix;
						factor = GribNumbers.int4(gribStream);
					}
					else if (gridDefinitionTemplateNumber == 53)
					{
						//Stretched and Rotated 
						// Spherical harmonic coefficients
						
						spLat = GribNumbers.int4(gribStream) * tenToNegSix;
						spLon = GribNumbers.int4(gribStream) * tenToNegSix;
                        rotationangle = GribNumbers.IEEEfloat4(gribStream);
						poleLat = GribNumbers.int4(gribStream) * tenToNegSix;
						poleLon = GribNumbers.int4(gribStream) * tenToNegSix;
						factor = GribNumbers.int4(gribStream);
					}
					break;
				
				
				case 90:  // Space view perspective or orthographic
					shape = gribStream.ReadByte();
					scaleFactorRadius = gribStream.ReadByte();
					scaledValueRadius = GribNumbers.int4(gribStream);
					scaleFactorMajor = gribStream.ReadByte();
					scaledValueMajor = GribNumbers.int4(gribStream);
					scaleFactorMinor = gribStream.ReadByte();
					scaledValueMinor = GribNumbers.int4(gribStream);
					nx = GribNumbers.int4(gribStream);
					ny = GribNumbers.int4(gribStream);
					lap = GribNumbers.int4(gribStream);
					lop = GribNumbers.int4(gribStream);
					resolution = gribStream.ReadByte();
					dx = GribNumbers.int4(gribStream);
					dy = GribNumbers.int4(gribStream);
					xp = (float) (GribNumbers.int4(gribStream) * tenToNegThree);
					yp = (float) (GribNumbers.int4(gribStream) * tenToNegThree);
					scanMode = gribStream.ReadByte();
					basicAngleOfLatLon = GribNumbers.int4(gribStream);
					altitude = GribNumbers.int4(gribStream) * 1000000;
					xo = GribNumbers.int4(gribStream);
					yo = GribNumbers.int4(gribStream);
					
					break;
				
				
				case 100:  // Triangular grid based on an icosahedron
					
					n2 = gribStream.ReadByte();
					n3 = gribStream.ReadByte();
					ni = GribNumbers.int2(gribStream);
					nd = gribStream.ReadByte();
					poleLat = GribNumbers.int4(gribStream) * tenToNegSix;
					poleLon = GribNumbers.int4(gribStream) * tenToNegSix;
					lonofcenter = GribNumbers.int4(gribStream);
					position = gribStream.ReadByte();
					order = gribStream.ReadByte();
					scanMode = gribStream.ReadByte();
					n = GribNumbers.int4(gribStream);
					break;
				
				
				case 110:  // Equatorial azimuthal equidistant projection
					shape = gribStream.ReadByte();
					scaleFactorRadius = gribStream.ReadByte();
					scaledValueRadius = GribNumbers.int4(gribStream);
					scaleFactorMajor = gribStream.ReadByte();
					scaledValueMajor = GribNumbers.int4(gribStream);
					scaleFactorMinor = gribStream.ReadByte();
					scaledValueMinor = GribNumbers.int4(gribStream);
					nx = GribNumbers.int4(gribStream);
					ny = GribNumbers.int4(gribStream);
					la1 = GribNumbers.int4(gribStream) * tenToNegSix;
					lo1 = GribNumbers.int4(gribStream) * tenToNegSix;
					resolution = gribStream.ReadByte();
					dx = (float) (GribNumbers.int4(gribStream) * tenToNegThree);
					dy = (float) (GribNumbers.int4(gribStream) * tenToNegThree);
					projectionCenter = gribStream.ReadByte();
					scanMode = gribStream.ReadByte();
					
					break;
				
				
				case 120:  // Azimuth-range Projection
					nb = GribNumbers.int4(gribStream);
					nr = GribNumbers.int4(gribStream);
					la1 = GribNumbers.int4(gribStream);
					lo1 = GribNumbers.int4(gribStream);
					dx = GribNumbers.int4(gribStream);
                    dstart = GribNumbers.IEEEfloat4(gribStream);
					scanMode = gribStream.ReadByte();
					for (int i = 0; i < nr; i++)
					{
						// get azi (33+4(Nr-1))-(34+4(Nr-1))
						// get adelta (35+4(Nr-1))-(36+4(Nr-1))
					}
					throw new GribNotSupportedException("need code to get azi and adelta");
					

                    // Comment in, when you remove the Exception, otherwise booom
				    // break; 
				
				
				default: 
					throw new GribNotSupportedException("Unknown Grid Type " + System.Convert.ToString(gridDefinitionTemplateNumber));
					
				
			} 
			
			// calculate earth radius
			if ((gridDefinitionTemplateNumber < 50 || gridDefinitionTemplateNumber > 53) && gridDefinitionTemplateNumber != 100 && gridDefinitionTemplateNumber != 120)
			{
				if (shape == 0)
				{
					earthRadius = 6367470;
				}
				else if (shape == 1)
				{
					earthRadius = scaledValueRadius;
					if (scaleFactorRadius != 0)
						earthRadius = (float) (earthRadius / System.Math.Pow(10, scaleFactorRadius));
				}
				else if (shape == 2)
				{
					majorAxis = (float) 6378160.0;
					minorAxis = (float) 6356775.0;
				}
				else if (shape == 3)
				{
					majorAxis = scaledValueMajor;
					majorAxis = (float) (majorAxis / System.Math.Pow(10, scaleFactorMajor));
					
					minorAxis = scaledValueMinor;
					minorAxis = (float) (minorAxis / System.Math.Pow(10, scaleFactorMinor));
				}
				else if (shape == 4)
				{
					majorAxis = (float) 6378137.0;
					minorAxis = (float) SupportClass.Identity(6356752.314);
				}
				else if (shape == 6)
				{
					earthRadius = 6371229;
				}
			}
		} // end of Grib2GridDefinitionSection
		
		/// <summary> .</summary>
		/// <param gridTemplateName="gridDefinitionTemplateNumber"> Grid definition template number same as type of grid
		/// </param>
		/// <returns> GridName as a String
		/// 
		/// </returns>
		public static System.String getGridName(int gdtn)
		{
			switch (gdtn)
			{
				
				// code table 3.2
				case 0:  return "Latitude/Longitude";
				
				case 1:  return "Rotated Latitude/Longitude";
				
				case 2:  return "Stretched Latitude/Longitude";
				
				case 3:  return "iStretched and Rotated Latitude/Longitude";
				
				case 10:  return "Mercator";
				
				case 20:  return "Polar stereographic";
				
				case 30:  return "Lambert Conformal";
				
				case 31:  return "Albers Equal Area";
				
				case 40:  return "Gaussian latitude/longitude";
				
				case 41:  return "Rotated Gaussian Latitude/longitude";
				
				case 42:  return "Stretched Gaussian Latitude/longitude";
				
				case 43:  return "Stretched and Rotated Gaussian Latitude/longitude";
				
				case 50:  return "Spherical harmonic coefficients";
				
				case 51:  return "Rotated Spherical harmonic coefficients";
				
				case 52:  return "Stretched Spherical harmonic coefficients";
				
				case 53:  return "Stretched and Rotated Spherical harmonic coefficients";
				
				case 90:  return "Space View Perspective or Orthographic";
				
				case 100:  return "Triangular Grid Based on an Icosahedron";
				
				case 110:  return "Equatorial Azimuthal Equidistant";
				
				case 120:  return "Azimuth-Range";
				
				
				default:  return "Unknown projection" + gdtn;
				
			}
		} 


		/// <summary> .</summary>
		/// <returns> shapeName as a String
		/// 
		/// </returns>
		public System.String getShapeName()
		{
			return getShapeName(shape);
		}
		
		/// <summary> .</summary>
		/// <param gridTemplateName="shape">
		/// </param>
		/// <returns> shapeName as a String
		/// 
		/// </returns>
		static public System.String getShapeName(int shape)
		{
			switch (shape)
			{
				
				// code table 3.2
				case 0:  return "Earth spherical with radius = 6367470 m";
				
				case 1:  return "Earth spherical with radius specified by producer";
				
				case 2:  return "Earth oblate spheroid with major axis = 6378160.0 m and minor axis = 6356775.0 m";
				
				case 3:  return "Earth oblate spheroid with axes specified by producer";
				
				case 4:  return "Earth oblate spheroid with major axis = 6378137.0 m and minor axis = 6356752.314 m";
				
				case 5:  return "Earth represent by WGS84";
				
				case 6:  return "Earth spherical with radius of 6371229.0 m";
				
				default:  return "Unknown Earth Shape";
				
			}
		}
	} // end Grib2GridDefinitionSection
}