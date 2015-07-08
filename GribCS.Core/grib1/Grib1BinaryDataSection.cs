/*
 * This file is part of GribCS.
 * This code is based on an automatic conversion of JGRIB Beta 7 
 * (http://jgrib.sourceforge.net/) from Java to C#.
 * 
 * C# code: Copyright 2006-2010 Seaware AB, PO Box 1244, SE-131 28 
 * Nacka Strand, Sweden, info@seaware.se.
 * 
 * Java-code: Copyright 1997-2006 Unidata Program Center/University 
 * Corporation for Atmospheric Research, P.O. Box 3000, Boulder, CO 80307,
 * support@unidata.ucar.edu.
 * 
 * GribCS is free software: you can redistribute it and/or modify it under 
 * the terms of the GNU Lesser General Public License as published by the 
 * Free Software Foundation, either version 3 of the License, or (at your 
 * option) any later version.
 * 
 * GribCS is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of 
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License 
 * along with GribCS.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using NGribCS;
using System.Runtime.InteropServices;
using NGribCS.Helpers;

namespace NGribCS.Grib1
{
	
	/// <summary> A class representing the binary data numberOfSection (BDS) of a GRIB record.
	/// 
	/// </summary>
	/// <version>  1.0
	/// </version>

    [GuidAttribute("FCD45927-A941-4ca9-9849-6140A3E58801")]
    [ClassInterface(ClassInterfaceType.None)]
	public sealed class Grib1BinaryDataSection : NGribCS.Grib1.IGrib1BinaryDataSection
	{
		/// <summary> Grid values as an array of float.
		/// 
		/// </summary>
		/// <returns>  array of grid values
		/// </returns>
		public float[] Values
		{
			get
			{
				return values;
			}
			
		}

        public static float MissingValue
        {
            get
            {
                return UNDEFINED;
            }
        }

		/// <summary> Constant value for an undefined grid value.</summary>
		private const float UNDEFINED = - 9999f;
		
		/// <summary> Length in bytes of this BDS.</summary>
		private int length;
		
		/// <summary> Buffer for one byte which will be processed bit by bit.</summary>
		private int bitBuf = 0;
		
		/// <summary> Current bit position in <tt>bitBuf</tt>.</summary>
		private int bitPos = 0;
		
		/// <summary> Array of grid values.</summary>
		private float[] values;
		
		/// <summary> Indicates whether the BMS is represented by a single value
		/// Octet 12 is empty, and the data is represented by the reference value.
		/// </summary>
		private bool isConstant = false;
		
		
		// *** constructors *******************************************************
		
		/// <summary> Constructs a Grib1BinaryDataSection object from a gribStream.
		/// A bit map is not available.
		/// 
		/// </summary>
		/// <param gridTemplateName="gribStream">RandomAccessFile stream with BDS content
		/// </param>
		/// <param gridTemplateName="decimalscale">the exponent of the decimal scale
		/// 
		/// </param>
		/// <throws>  NotSupportedException  if stream contains no valid GRIB file </throws>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public Grib1BinaryDataSection(System.IO.Stream raf, int decimalscale):this(raf, decimalscale, null)
		{
		}
		
		/// <summary> Constructs a Grib1BinaryDataSection object from a gribStream.
		/// A bit map is defined.
		/// 
		/// </summary>
		/// <param gridTemplateName="gribStream">gribStream with BDS content
		/// </param>
		/// <param gridTemplateName="decimalscale">the exponent of the decimal scale
		/// </param>
		/// <param gridTemplateName="bms">bit map numberOfSection of GRIB record
		/// 
		/// </param>
		/// <throws>  NotSupportedException  if stream contains no valid GRIB file </throws>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public Grib1BinaryDataSection(System.IO.Stream raf, int decimalscale, Grib1BitMapSection bms)
		{
			// octets 1-3 (numberOfSection lengthOfSection)
			length = (int)GribNumbers.uint3(raf);
			//System.out.println( "BDS lengthOfSection = " + lengthOfSection );
			
			// octet 4, 1st half (packing flag)
			int unusedbits = raf.ReadByte();

            // TODO Check this!!!
			if ((unusedbits & 192) != 0)
				throw new NGribCS.Helpers.GribNotSupportedException("BDS: (octet 4, 1st half) not grid point data and simple packing ");
			
			// octet 4, 2nd half (number of unused bits at end of this numberOfSection)
			unusedbits = unusedbits & 15;
			//System.out.println( "BDS unusedbits = " + unusedbits );
			
			// octets 5-6 (binary scale factor)
			int binscale = GribNumbers.int2(raf);
			
			// octets 7-10 (reference point = minimum value)
			float refvalue = GribNumbers.float4(raf);
			
			// octet 11 (number of bits per value)
			int numbits = raf.ReadByte();
			//System.out.println( "BDS numbits = " + numbits );
			if (numbits == 0)
				isConstant = true;
			//System.out.println( "BDS isConstant = " + isConstant );
			
			// *** read values *******************************************************
			
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			float ref_Renamed = (float) (System.Math.Pow(10.0, - decimalscale) * refvalue);
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			float scale = (float) (System.Math.Pow(10.0, - decimalscale) * System.Math.Pow(2.0, binscale));
			
			if (bms != null)
			{
				bool[] bitmap = bms.Bitmap;
				
				values = new float[bitmap.Length];
				for (int i = 0; i < bitmap.Length; i++)
				{
					if (bitmap[i])
					{
						if (!isConstant)
						{
							values[i] = ref_Renamed + scale * bits2UInt(numbits, raf);
						}
						else
						{
							// rdg - added this to handle a constant valued parameter
							values[i] = ref_Renamed;
						}
					}
					else
						values[i] = Grib1BinaryDataSection.UNDEFINED;
				}
			}
			else
			{
				// bms is null
				if (!isConstant)
				{
					//System.out.println( "BDS values.size = " + 
					//(((lengthOfSection - 11) * 8 - unusedbits) /  numbits));
					values = new float[((length - 11) * 8 - unusedbits) / numbits];
					
					for (int i = 0; i < values.Length; i++)
					{
						values[i] = ref_Renamed + scale * bits2UInt(numbits, raf);
					}
				}
				else
				{
					// constant valued - same min and max
					int x = 0, y = 0;
					raf.Seek(raf.Position - 53, System.IO.SeekOrigin.Begin); // return to start of GDS
                    length = (int)GribNumbers.uint3(raf);
					if (length == 42)
					{
						// Lambert/Mercator offset
						SupportClass.Skip(raf, 3);
                        x = GribNumbers.int2(raf);
                        y = GribNumbers.int2(raf);
					}
					else
					{
						SupportClass.Skip(raf, 7);
                        length = (int)GribNumbers.uint3(raf);
						if (length == 32)
						{
							// Polar sterographic
							SupportClass.Skip(raf, 3);
                            x = GribNumbers.int2(raf);
                            y = GribNumbers.int2(raf);
						}
						else
						{
							x = y = 1;
							System.Console.Out.WriteLine("BDS constant value, can't determine array size");
						}
					}
					values = new float[x * y];
					for (int i = 0; i < values.Length; i++)
						values[i] = ref_Renamed;
				}
			}
		} // end Grib1BinaryDataSection
		
		/// <summary> Convert bits (numberOfBits) to Unsigned Int .
		/// 
		/// </summary>
		/// <param gridTemplateName="numberOfBits">
		/// </param>
		/// <param gridTemplateName="gribStream">
		/// </param>
		/// <returns> int of BinaryDataSection numberOfSection
		/// </returns>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		private int bits2UInt(int nb, System.IO.Stream raf)
		{
			int bitsLeft = nb;
			int result = 0;
			
			if (bitPos == 0)
			{
				bitBuf = raf.ReadByte();
				bitPos = 8;
			}
			
			while (true)
			{
				int shift = bitsLeft - bitPos;
				if (shift > 0)
				{
					// Consume the entire buffer
					result |= bitBuf << shift;
					bitsLeft -= bitPos;
					
					// Get the next byte from the RandomAccessFile
					bitBuf = raf.ReadByte();
					bitPos = 8;
				}
				else
				{
					// Consume a portion of the buffer
					result |= bitBuf >> - shift;
					bitPos -= bitsLeft;
					bitBuf &= 0xff >> (8 - bitPos); // mask off consumed bits
					
					return result;
				}
			} // end while
		} // end bits2Int
		
		// *** public methods ****************************************************
		
		// --Commented out by Inspection START (11/17/05 1:25 PM):
		//   /**
		//    * Get the lengthOfSection in bytes of this numberOfSection.
		//    *
		//    * @return lengthOfSection in bytes of this numberOfSection
		//    */
		//   public int getLength()
		//   {
		//      return lengthOfSection;
		//   }
		// --Commented out by Inspection STOP (11/17/05 1:25 PM)
	} // end class Grib1BinaryDataSection
}