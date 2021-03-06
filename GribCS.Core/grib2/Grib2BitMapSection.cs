
/*
 * This file is part of NGribCS which is a fork of GribCS
 * found at http://sourceforge.net/projects/gribcs/ 
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

using System;
using System.Runtime.InteropServices;
using NGribCS.Helpers;


namespace NGribCS.Grib2
{
	
	
	/// <summary> A class that represents the BitMapSection of a GRIB product.
	/// 
	/// </summary>
	/// <author>   Robb Kambic
	/// </author>
	/// <version>  1.0
	/// 
	/// </version>
    [ComVisible(false)]
	public sealed class Grib2BitMapSection
	{
		/// <summary> Get bit map.
		/// 
		/// </summary>
		/// <returns> bit map as array of boolean values
		/// </returns>
		public bool[] Bitmap
		{
			get
			{
				return this.bitmap;
			}
			
		}
		
		/// <summary> Length in bytes of BitMapSection numberOfSection.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'lengthOfSection '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int length;
		
		/// <summary> Number of this numberOfSection, should be 6.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'numberOfSection '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int section;
		
		/// <summary> Bit-map indicator (see Code Table 6.0 and Note (1))</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'bitMapIndicator '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int bitMapIndicator;
		
		/// <summary> The bit map.</summary>
		private bool[] bitmap = null;
		

        public int BitMapIndicator
        {
            get
            {
                return bitMapIndicator;
            }
        }

		// *** constructors *******************************************************
		
		/// <summary> Constructs a <tt>Grib2BitMapSection</tt> object from a byteBuffer.
		/// 
		/// </summary>
		/// <param gridTemplateName="gribStream">RandomAccessFile with Section BMS content
		/// </param>
		/// <param gridTemplateName="gds">Grib2GridDefinitionSection
		/// </param>
		/// <throws>  IOException  if stream contains no valid GRIB file </throws>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public Grib2BitMapSection(System.IO.Stream raf, Grib2GridDefinitionSection gds)
		{
			int[] bitmask = new int[]{128, 64, 32, 16, 8, 4, 2, 1};
			
			// octets 1-4 (Length of BMS)
			length = GribNumbers.int4(raf);
			//System.out.println( "BMS lengthOfSection=" + lengthOfSection );
			
			section = raf.ReadByte();
			//System.out.println( "BMS is 6, numberOfSection=" + numberOfSection );
			
			bitMapIndicator = raf.ReadByte();
			//System.out.println( "BMS bitMapIndicator=" + bitMapIndicator );
			
			// no bitMap
			if (bitMapIndicator != 0)
				return ;
			
			sbyte[] data = new sbyte[this.length - 6];
			SupportClass.ReadInput(raf, data, 0, data.Length);
			
			// create new bit map, octet 4 contains number of unused bits at the end
			this.bitmap = new bool[gds.NumberOfDataPoints];
			//System.out.println( "BMS GDS NumberOfDataPoints = " + gds.getNumberPoints() );
			//System.out.println( "BMS bitmap.lengthOfSection = " + this.bitmap.lengthOfSection );
			
			// fill bit map
			for (int i = 0; i < this.bitmap.Length; i++)
				this.bitmap[i] = (data[i / 8] & bitmask[i % 8]) != 0;
		}
		
		// --Commented out by Inspection START (12/8/05 1:12 PM):
		//   /**
		//    * Get the byte lengthOfSection of the BitMapSection numberOfSection.
		//    *
		//    * @return lengthOfSection in bytes of BitMapSection numberOfSection
		//    */
		//   public final int getLength()
		//   {
		//      return lengthOfSection;
		//   }
		// --Commented out by Inspection STOP (12/8/05 1:12 PM)
	}
}