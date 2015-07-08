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
namespace NGribCS.Grib1
{
	
	/// <summary> A class that represents the EndSection of a GRIB1 product.
	/// 
	/// </summary>
	
	sealed class Grib1EndSection
	{
		/// <summary> Get ending flag for Grib record.
		/// 
		/// </summary>
		/// <returns> true if  "7777" found
		/// </returns>
		public bool EndFound
		{
			get
			{
				return endFound;
			}
			
			// --Commented out by Inspection START (11/17/05 1:32 PM):
			//   /**
			//    * how long was the ending, should be 4 bytes.
			//    * @return int
			//   */
			//   public static final int getLength()
			//   {
			//      return lengthOfSection;
			//   }
			// --Commented out by Inspection STOP (11/17/05 1:32 PM)
			
		}
		/*
		* was the grib endding 7777 found.
		*/
		private bool endFound = false;
		
		/*
		* how long was the ending, should be 4 bytes.
		*/
		private int length = 0;
		
		// *** constructors *******************************************************
		
		/// <summary> Constructs a <tt>Grib1EndSection</tt> object from a byteBuffer.
		/// 
		/// </summary>
		/// <param gridTemplateName="gribStream">RandomAccessFile with EndSection content
		/// 
		/// </param>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public Grib1EndSection(System.IO.Stream raf)
		{
			int match = 0;
			while (raf.Position < raf.Length)
			{
				// code must be "7" "7" "7" "7"
				sbyte c = (sbyte) raf.ReadByte();
				//System.out.println( "c=" + (char) c );
				length++;
				if (c == '7')
				{
					match += 1;
					//System.out.println( "seekEnd gribStream.Position=" + gribStream.Position );
				}
				else
				{
					//System.out.println( "c=" + (char) c );
					match = 0; /* Needed to protect against bad ending case. */
				}
				if (match == 4)
				{
					endFound = true;
					//System.out.println( "7777 ending found" );
					break;
				}
			}
		} // end Grib1EndSection
	} // end Grib1EndSection
}