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
	
	/// <summary> A class that represents the bitmap numberOfSection (BMS) of a GRIB record. It
	/// indicates grid points where no grid value is defined by a 0.
	/// 
	/// </summary>
	/// <version>  1.0
	/// </version>
    [GuidAttribute("5114A977-78E8-4e8e-967C-2D34A1F491CA")]
    [ClassInterface(ClassInterfaceType.None)]
	public sealed class Grib1BitMapSection : NGribCS.Grib1.IGrib1BitMapSection
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
				return bitmap;
			}
			
		}
		
		/// <summary> Length in bytes of this numberOfSection.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'lengthOfSection '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int length;
		
		/// <summary> The bit map.</summary>
		private bool[] bitmap;
		
		
		// *** constructors *******************************************************
		
		/// <summary> Constructs a <tt> Grib1BitMapSection</tt> object from a gribStream input stream.
		/// 
		/// </summary>
		/// <param gridTemplateName="gribStream">input stream with BMS content
		/// 
		/// </param>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public Grib1BitMapSection(System.IO.Stream raf)
		{
			int[] bitmask = new int[]{128, 64, 32, 16, 8, 4, 2, 1};
			
			// octet 1-3 (lengthOfSection of numberOfSection)
            length = (int)GribNumbers.uint3(raf);
			//System.out.println( "BMS lengthOfSection = " + lengthOfSection );
			
			// octet 4 unused bits
			int unused = raf.ReadByte();
			//System.out.println( "BMS unused = " + unused );
			
			// octets 5-6
            int bm = GribNumbers.int2(raf);
			if (bm != 0)
			{
				System.Console.Out.WriteLine("BMS pre-defined BM provided by center");
				if ((length - 6) == 0)
					return ;
				sbyte[] data = new sbyte[length - 6];
				SupportClass.ReadInput(raf, data, 0, data.Length);
				return ;
			}
			sbyte[] data2 = new sbyte[length - 6];
			SupportClass.ReadInput(raf, data2, 0, data2.Length);
			
			// create new bit map, octet 4 contains number of unused bits at the end
			bitmap = new bool[(length - 6) * 8 - unused];
			//System.out.println( "BMS bitmap.lengthOfSection = " + bitmap.lengthOfSection );
			
			// fill bit map
			for (int i = 0; i < bitmap.Length; i++)
				bitmap[i] = (data2[i / 8] & bitmask[i % 8]) != 0;
		} // end Grib1BitMapSection
		
		// *** public methods ****************************************************
		
		// --Commented out by Inspection START (11/17/05 1:25 PM):
		//   /**
		//    * Get lengthOfSection in bytes of this numberOfSection.
		//    *
		//    * @return lengthOfSection in bytes
		//    */
		//   public int getLength()
		//   {
		//      return lengthOfSection;
		//   }
		// --Commented out by Inspection STOP (11/17/05 1:25 PM)
	} // end Grib1BitMapSection
}