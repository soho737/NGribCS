using NGribCS.Helpers;
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
using System.Runtime.InteropServices;

namespace NGribCS.Grib2
{
	
	/// <summary> A class that represents the DataSection of a GRIB product.
	/// 
	/// </summary>
	[ComVisible(false)]
	public sealed class Grib2DataSection
	{
		/// <summary> Grib2 data unpacked as floats.</summary>
		/// <returns> data
		/// </returns>
		public float[] Data
		{
			get
			{
				return data;
			}
			
		}
		/// <summary> Length in bytes of DataSection numberOfSection.</summary>
		private int length;
		
		/// <summary> Number of this numberOfSection, should be 7.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'numberOfSection '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int section;
		
		/// <summary> Data Array used to return unpacked values.</summary>
		private float[] data;
		
		/// <summary> Buffer for one byte which will be processed bit by bit.</summary>
		private int bitBuf = 0;
		
		/// <summary> Current bit position in <tt>bitBuf</tt>.</summary>
		private int bitPos = 0;
		private int scanMode;
		private int count; // raw data count
		private int Xlength; // lengthOfSection of the x axis
		
		// *** constructors *******************************************************
		
		/// <summary> Constructor for a Grib2 Data Section.</summary>
		/// <param gridTemplateName="getData">
		/// </param>
		/// <param gridTemplateName="gribStream">
		/// </param>
		/// <param gridTemplateName="gds">
		/// </param>
		/// <param gridTemplateName="drs">
		/// </param>
		/// <param gridTemplateName="bms">
		/// </param>
		/// <throws>  IOException </throws>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public Grib2DataSection(bool getData, System.IO.Stream raf, Grib2GridDefinitionSection gds, Grib2DataRepresentationSection drs, Grib2BitMapSection bms)
		{
			//System.out.println( "gribStream.FilePointer=" + gribStream.FilePointer() );
			// octets 1-4 (Length of DS)
			length = GribNumbers.int4(raf);
			//System.out.println( "DS lengthOfSection=" + lengthOfSection );
			//System.out.println( "DS calculated end=" + ( gribStream.getFilePointer() + lengthOfSection -4 ));
			// octet 5  numberOfSection 7
			section = raf.ReadByte();
			//System.out.println( "DS is 7, numberOfSection=" + numberOfSection );
			if (!getData)
			{
				// skip data read
				//System.out.println( "gribStream.position before reposition="+gribStream.getFilePointer());
				//System.out.println( "gribStream.lengthOfSection=" + gribStream.lengthOfSection() );
				// sanity check for erronous descriptorSpatial lengthOfSection
				if (length > 0 && length < raf.Length)
				{
					SupportClass.Skip(raf, length - 5);
					//System.out.println( "gribStream.skipBytes = " + (lengthOfSection -5) );
				}
				else
				{
					length = 5; // only read lengthOfSection and numberOfSection
				}
				//System.out.println( "gribStream.position after skip=" + gribStream.getFilePointer() );
				return ;
			}
			int dtn = drs.DataTemplateNumber;
			//System.out.println( "DS dtn=" + dtn );
			if (dtn == 0 || dtn == 1)
			{
				// 0: Grid point data - simple packing
				// 1: Matrix values - simple packing
				simpleUnpacking(raf, gds, drs, bms);
			}
			else if (dtn == 2)
			{
				// 2:Grid point data - complex packing
				complexUnpacking(raf, gds, drs, bms);
			}
			else if (dtn == 3)
			{
				// 3: complex packing with spatial differencing
				complexUnpackingWithSpatial(raf, gds, drs, bms);
			}
			else if (dtn == 40 || dtn == 40000)
			{
				// JPEG 2000 Stream Format
				jpeg2000Unpacking(raf, gds, drs, bms);
			}
		} // end Grib2DataSection
		
		/// <summary> simple Unpacking method for Grib2 data.</summary>
		/// <param gridTemplateName="gribStream">
		/// </param>
		/// <param gridTemplateName="gds">
		/// </param>
		/// <param gridTemplateName="drs">
		/// </param>
		/// <param gridTemplateName="bms">
		/// </param>
		/// <throws>  IOException </throws>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		private void  simpleUnpacking(System.IO.Stream raf, Grib2GridDefinitionSection gds, Grib2DataRepresentationSection drs, Grib2BitMapSection bms)
		{
            if (bms.BitMapIndicator != 255 && bms.BitMapIndicator != 0)
                throw new NotImplementedException("Simple unpacking with a bitmap indicator other than 0 or 255 is currently not supported");

			int dtn = drs.DataTemplateNumber;
			//System.out.println( "DS dtn=" + dtn );
			
			if (dtn == 1)
			{
				// Matrix values
				System.Console.Out.WriteLine("DS Matrix values not supported yet");
				return ;
			}
			// dataPoints are number of points encoded, it could be less than the
			// numberOfDataPoints in the grid record if bitMap is used, otherwise equal
			int dataPoints = drs.DataPoints;
			//System.out.println( "DS DRS dataPoints=" + drs.getDataPoints() );
			float pmv = drs.PrimaryMissingValue;
			//System.out.println( "DS pmv=" + pmv );
			int nb = drs.NumberOfBits;
			//System.out.println( "DS numberOfBits=" + numberOfBits );
			int D = drs.DecimalScaleFactor;
			//System.out.println( "DS D=" + D );
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			float DD = (float) System.Math.Pow((double) 10, (double) D);
			//System.out.println( "DS DD=" + DD );
			float R = drs.ReferenceValue;
			//System.out.println( "DS R=" + R );
			int E = drs.BinaryScaleFactor;
			//System.out.println( "DS E=" + E );
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			float EE = (float) System.Math.Pow((double) 2.0, (double) E);
			//System.out.println( "DS EE=" + EE );
			
			int numberPoints = gds.NumberOfDataPoints;
			//System.out.println( "DS GDS NumberOfDataPoints=" +  gds.getNumberPoints() );
			data = new float[numberPoints];
			
			bool[] bitmap = bms.Bitmap;
			
			//  Y * 10**D = R + (X1 + X2) * 2**E
			//   E = binary scale factor
			//   D = decimal scale factor
			//   R = reference value
			//   X1 = 0
			//   X2 = scaled encoded value
			//   data[ pInvItem ] = (R + ( X1 + X2) * EE)/DD ;
			
			if (bitmap == null)
			{
				for (int i = 0; i < numberPoints; i++)
				{
					//data[ pInvItem ] = (R + ( X1 + X2) * EE)/DD ;
					data[i] = (R + bits2UInt(nb, raf) * EE) / DD;
				}
			}
			else
			{
				bitPos = 0;
				bitBuf = 0;
				for (int i = 0; i < bitmap.Length; i++)
				{
					if (bitmap[i])
					{
						//data[ pInvItem ] = (R + ( X1 + X2) * EE)/DD ;
						data[i] = (R + bits2UInt(nb, raf) * EE) / DD;
					}
					else
					{
						data[i] = pmv;
					}
				}
			}
			scanMode = gds.ScanMode;
			Xlength = gds.Nx; // needs some smarts for different type Grids
			//scanningModeCheck();
		} // end simpleUnpacking
		
		/// <summary> complex unpacking of Grib2 data.</summary>
		/// <param gridTemplateName="gribStream">
		/// </param>
		/// <param gridTemplateName="gds">
		/// </param>
		/// <param gridTemplateName="drs">
		/// </param>
		/// <throws>  IOException </throws>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		private void  complexUnpacking(System.IO.Stream raf, Grib2GridDefinitionSection gds, Grib2DataRepresentationSection drs, Grib2BitMapSection bms)
		{
            if (bms.BitMapIndicator != 255)
                throw new NotImplementedException("Complex unpacking with a bitmap is currently not supported");

			int mvm = drs.MissingValueManagement;
			//System.out.println( "DS mvm=" + mvm );
			
			float pmv = drs.PrimaryMissingValue;
			//System.out.println( "DS pmv=" + pmv );
			
			int NG = drs.NumberOfGroups;
			//System.out.println( "DS numGroups=" + numGroups );
			
			// 6-xx  Get reference values for groups (X1's)
			int[] X1 = new int[NG];
			int nb = drs.NumberOfBits;
			//System.out.println( "DS numberOfBits=" + numberOfBits );
			bitPos = 0;
			bitBuf = 0;
			for (int i = 0; i < NG; i++)
			{
				X1[i] = bits2UInt(nb, raf);
				//System.out.println( "DS X1[ pInvItem ]=" + X1[ pInvItem ] );
			}
			
			// [xx +1 ]-yy Get number of bits used to encode each group
			int[] NB = new int[NG];
			nb = drs.BitsGroupWidths;
			//System.out.println( "DS numberOfBits=" + numberOfBits );
			bitPos = 0;
			bitBuf = 0;
			for (int i = 0; i < NG; i++)
			{
				NB[i] = bits2UInt(nb, raf);
				//System.out.println( "DS numBitsEncodingEachGroup[ pInvItem ]=" + numBitsEncodingEachGroup[ pInvItem ] );
			}
			
			// [yy +1 ]-zz Get the scaled group lengths using formula
			//     Ln = ref + Kn * lengthIncrement, where n = 1-numGroups,
			//          ref = referenceGroupLength, and  lengthIncrement = lengthIncrement
			
			int[] L = new int[NG];
			int countL = 0;
			int ref_Renamed = drs.ReferenceGroupLength;
			//System.out.println( "DS ref=" + ref );
			int len_inc = drs.LengthIncrement;
			//System.out.println( "DS lengthIncrement=" + lengthIncrement );
			nb = drs.BitsScaledGroupLength;
			//System.out.println( "DS numberOfBits=" + numberOfBits );
			bitPos = 0;
			bitBuf = 0;
			for (int i = 0; i < NG; i++)
			{
				// numGroups
				L[i] = ref_Renamed + (bits2UInt(nb, raf) * len_inc);
				//System.out.println( "DS L[ pInvItem ]=" + L[ pInvItem ] );
				countL += L[i];
			}
			//System.out.println( "DS countL=" + countL );
			
			// [zz +1 ]-nn get X2 values and calculate the results Y using formula
			//                Y * 10**D = R + (X1 + X2) * 2**E
			
			int D = drs.DecimalScaleFactor;
			//System.out.println( "DS D=" + D );
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			float DD = (float) System.Math.Pow((double) 10, (double) D);
			//System.out.println( "DS DD=" + DD );
			
			float R = drs.ReferenceValue;
			//System.out.println( "DS R=" + R );
			
			int E = drs.BinaryScaleFactor;
			//System.out.println( "DS E=" + E );
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			float EE = (float) System.Math.Pow((double) 2.0, (double) E);
			//System.out.println( "DS EE=" + EE );
			
			data = new float[countL];
			//System.out.println( "DS countL=" + countL + " dataPoints=" +
			//gds.getNumberPoints() );
			count = 0;
			Xlength = gds.Nx; // needs some smarts for different type Grids
			// used to check missing values when X2 is packed with all 1's
			int[] bitsmv1 = new int[31];
			//int bitsmv2[] = new int[ 31 ]; didn't code cuz number larger the # of bits
			for (int i = 0; i < 31; i++)
			{
				//bitsmv1[ pInvItem ] = ( bitsmv1[ pInvItem -1 ] +1 ) *2 -1;
				//bitsmv2[ pInvItem ] = ( bitsmv2[ pInvItem -1 ] +2 ) *2 -2;
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				bitsmv1[i] = (int) System.Math.Pow((double) 2, (double) i) - 1;
				//bitsmv2[ pInvItem ] = (int) java.lang.Math.pow( (double)2, (double)pInvItem +1) -2;
				//System.out.println( "DS bitsmv1[ "+ pInvItem +" ] =" + bitsmv1[ pInvItem ] );
				//System.out.println( "DS bitsmv2[ "+ pInvItem +" ] =" + bitsmv2[ pInvItem ] );
			}
			int X2;
			bitPos = 0;
			bitBuf = 0;
			for (int i = 0; i < NG - 1; i++)
			{
				//System.out.println( "DS numBitsEncodingEachGroup[ pInvItem ]=" + numBitsEncodingEachGroup[ pInvItem ] );
				//System.out.println( "DS L[ pInvItem ]=" + L[ pInvItem ] );
				//System.out.println( "DS X1[ pInvItem ]=" + X1[ pInvItem ] );
				for (int j = 0; j < L[i]; j++)
				{
					if (NB[i] == 0)
					{
						if (mvm == 0)
						{
							// X2 = 0
							data[count++] = (R + X1[i] * EE) / DD;
						}
						else if (mvm == 1)
						{
							data[count++] = pmv;
						}
					}
					else
					{
						X2 = bits2UInt(NB[i], raf);
						if (mvm == 0)
						{
							data[count++] = (R + (X1[i] + X2) * EE) / DD;
						}
						else if (mvm == 1)
						{
							// X2 is also set to missing value is all bits set to 1's
							if (X2 == bitsmv1[NB[i]])
							{
								data[count++] = pmv;
							}
							else
							{
								data[count++] = (R + (X1[i] + X2) * EE) / DD;
							}
						}
						//System.out.println( "DS count=" + count );
						//System.out.println( "DS numBitsEncodingEachGroup[ "+ pInvItem +" ]=" + numBitsEncodingEachGroup[ pInvItem ] );
						//System.out.println( "DS X1[ "+ pInvItem +" ]=" + X1[ pInvItem ] );
						//System.out.println( "DS X2 =" +X2 );
						//System.out.println( "DS X1[ pInvItem ] + X2 ="+(X1[ pInvItem ]+X2) );
					}
				} // end for j
			} // end for pInvItem
			// process last group
			int last = drs.LengthLastGroup;
			//System.out.println( "DS last=" + last );
			for (int j = 0; j < last; j++)
			{
				// last group
				if (NB[NG - 1] == 0)
				{
					if (mvm == 0)
					{
						// X2 = 0
						data[count++] = (R + X1[NG - 1] * EE) / DD;
					}
					else if (mvm == 1)
					{
						data[count++] = pmv;
					}
				}
				else
				{
					X2 = bits2UInt(NB[NG - 1], raf);
					if (mvm == 0)
					{
						data[count++] = (R + (X1[NG - 1] + X2) * EE) / DD;
					}
					else if (mvm == 1)
					{
						// X2 is also set to missing value is all bits set to 1's
						if (X2 == bitsmv1[NB[NG - 1]])
						{
							data[count++] = pmv;
						}
						else
						{
							data[count++] = (R + (X1[NG - 1] + X2) * EE) / DD;
						}
					}
				}
			} // end for j
			scanMode = gds.ScanMode;
			scanningModeCheck();
			//System.out.println( "DS true end =" + gribStream.position() );
		} // end complexUnpacking
		
		/// <summary> complex unpacking method for spatial data.</summary>
		/// <param gridTemplateName="gribStream">
		/// </param>
		/// <param gridTemplateName="gds">
		/// </param>
		/// <param gridTemplateName="drs">
		/// </param>
		/// <throws>  IOException </throws>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		private void  complexUnpackingWithSpatial(System.IO.Stream raf, Grib2GridDefinitionSection gds, Grib2DataRepresentationSection drs, Grib2BitMapSection bms)
		{
            if (bms.BitMapIndicator != 255)
                throw new NotImplementedException("Complex unpacking with spatial and a bitmap is currently not supported");

             /*
             * 0 - No explicit missing values included within the data values
             * 1 - Primary missing values included within the data values
             * 2 - Primary and secondary missing values included within the data values
             */
			int missingValManagement = drs.MissingValueManagement;
			

			float primaryMissingValue = drs.PrimaryMissingValue;
			
			int numGroups = drs.NumberOfGroups;
			//System.out.println( "DS numGroups=" + numGroups );
			
			int g1 = 0, gMin = 0, h1 = 0, h2 = 0, hMin = 0;
			// [6-ww]   1st values of undifferenced scaled values and minimums

            // 1 - First-Order Spatial Differencing
            // 2 - Second-Order Spatial Differencing
			int orderSpatial = drs.OrderSpatial;

            // Number of octets required in the data numberOfSection to specify extra descriptors needed for spatial differencing
			int descriptorSpatial = drs.DescriptorSpatial;
			//System.out.println( "DS orderSpatial=" + orderSpatial +" descriptorSpatial =" + descriptorSpatial );
			bitPos = 0;
			bitBuf = 0;
			int sign;
			// descriptorSpatial is number of bytes, convert to bits -1 for sign bit
			descriptorSpatial = descriptorSpatial * 8 - 1;
			if (orderSpatial == 1)
			{
				// first order spatial differencing g1 and gMin
				sign = bits2UInt(1, raf);
				g1 = bits2UInt(descriptorSpatial, raf);
				if (sign == 1)
				{
					g1 *= (- 1);
				}
				sign = bits2UInt(1, raf);
				gMin = bits2UInt(descriptorSpatial, raf);
				if (sign == 1)
				{
					gMin *= (- 1);
				}
			}
			else if (orderSpatial == 2)
			{
				//second order spatial differencing h1, h2, hMin
				sign = bits2UInt(1, raf);
				h1 = bits2UInt(descriptorSpatial, raf);
				if (sign == 1)
				{
					h1 *= (- 1);
				}
				sign = bits2UInt(1, raf);
				h2 = bits2UInt(descriptorSpatial, raf);
				if (sign == 1)
				{
					h2 *= (- 1);
				}
				sign = bits2UInt(1, raf);
				hMin = bits2UInt(descriptorSpatial, raf);
				if (sign == 1)
				{
					hMin *= (- 1);
				}
				//System.out.println( "DS descriptorSpatial ="+ descriptorSpatial +" h1=" + h1 +" h2 =" + h2 + " hMin=" + hMin );
			}
			else
			{
				System.Console.Out.WriteLine("DS error os=" + orderSpatial + " ds =" + descriptorSpatial);
				return ;
			}
			
			// [ww +1]-xx  Get reference values for groups (X1's)
			int[] X1 = new int[numGroups];
			int numberOfBits = drs.NumberOfBits;
			//System.out.println( "DS numberOfBits=" + numberOfBits );
			bitPos = 0;
			bitBuf = 0;
			for (int i = 0; i < numGroups; i++)
			{
				X1[i] = bits2UInt(numberOfBits, raf);
				//System.out.println( "DS X1[ pInvItem ]=" + X1[ pInvItem ] );
			}
			
			// [xx +1 ]-yy Get number of bits used to encode each group
			int[] numBitsEncodingEachGroup = new int[numGroups];
			numberOfBits = drs.BitsGroupWidths;
			//System.out.println( "DS numberOfBits=" + numberOfBits );
			bitPos = 0;
			bitBuf = 0;
			for (int i = 0; i < numGroups; i++)
			{
				numBitsEncodingEachGroup[i] = bits2UInt(numberOfBits, raf);
			}
			
			// [yy +1 ]-zz Get the scaled group lengths using formula
			//     Ln = ref + Kn * lengthIncrement, where n = 1-numGroups,
			//          ref = referenceGroupLength, and  lengthIncrement = lengthIncrement
			
			int[] L = new int[numGroups];
			int countL = 0;
			int referenceGroupLength = drs.ReferenceGroupLength;
			//System.out.println( "DS ref=" + ref );
			int lengthIncrement = drs.LengthIncrement;
			//System.out.println( "DS lengthIncrement=" + lengthIncrement );
			numberOfBits = drs.BitsScaledGroupLength;
			//System.out.println( "DS numberOfBits=" + numberOfBits );
			bitPos = 0;
			bitBuf = 0;
			for (int i = 0; i < numGroups; i++)
			{
				// numGroups
				L[i] = referenceGroupLength + (bits2UInt(numberOfBits, raf) * lengthIncrement);
				//System.out.println( "DS L[ pInvItem ]=" + L[ pInvItem ] );
				countL += L[i];
			}
			//System.out.println( "DS countL=" + countL );
			
			// [zz +1 ]-nn get X2 values and add X1[ pInvItem ] + X2

            countL += drs.LengthLastGroup;


                data = new float[countL];
       
			//System.out.println( "DS countL=" + countL + " dataPoints=" +
			//gds.getNumberPoints() );
			// used to check missing values when X2 is packed with all 1's
			int[] bitsmv1 = new int[31];
			//int bitsmv2[] = new int[ 31 ]; didn't code cuz number larger the # of bits
			for (int i = 0; i < 31; i++)
			{
				//bitsmv1[ pInvItem ] = ( bitsmv1[ pInvItem -1 ] +1 ) *2 -1;
				//bitsmv2[ pInvItem ] = ( bitsmv2[ pInvItem -1 ] +2 ) *2 -2;
				bitsmv1[i] = (int) System.Math.Pow((double) 2, (double) i) - 1;
				//bitsmv2[ pInvItem ] = (int) java.lang.Math.pow( (double)2, (double)pInvItem +1) -2;
				//System.out.println( "DS bitsmv1[ "+ pInvItem +" ] =" + bitsmv1[ pInvItem ] );
				//System.out.println( "DS bitsmv2[ "+ pInvItem +" ] =" + bitsmv2[ pInvItem ] );
			}
			count = 0;
			Xlength = gds.Nx; // needs some smarts for different type Grids
			int X2;
			bitPos = 0;
			bitBuf = 0;


			for (int i = 0; i < numGroups - 1; i++)
			{
				for (int j = 0; j < L[i]; j++)
				{
                   

					if (numBitsEncodingEachGroup[i] == 0)
					{                       
						if (missingValManagement == 0)
						{
							// X2 = 0                 
							data[count++] = X1[i];
						}
						else if (missingValManagement == 1)
						{                     
							data[count++] = primaryMissingValue;
						}
					}
					else
					{
						X2 = bits2UInt(numBitsEncodingEachGroup[i], raf);
						
						if (missingValManagement == 0)
						{
							data[count++] = X1[i] + X2;
						}
						else if (missingValManagement == 1)
						{
							// X2 is also set to missing value is all bits set to 1's
							if (X2 == bitsmv1[numBitsEncodingEachGroup[i]])
							{
								data[count++] = primaryMissingValue;
							}
							else
							{
								data[count++] = X1[i] + X2;
							}
						}
						//if( count > 1235 && count < 1275 ) {
						//   System.out.println( "DS count=" + count );
						//   System.out.println( "DS numBitsEncodingEachGroup[ "+ pInvItem +" ]=" + numBitsEncodingEachGroup[ pInvItem ] );
						//   System.out.println( "DS X1[ "+ pInvItem +" ]=" + X1[ pInvItem ] );
						//   System.out.println( "DS X2 =" +X2 );
						//   System.out.println( "DS X1[ pInvItem ] + X2 ="+(X1[ pInvItem ]+X2) );
						//}
					}
				} // end for j
			} // end for pInvItem
			// process last group
			int last = drs.LengthLastGroup;
			//System.out.println( "DS last=" + last );
			for (int j = 0; j < last; j++)
			{
               

				// last group
				if (numBitsEncodingEachGroup[numGroups - 1] == 0)
				{
					if (missingValManagement == 0)
					{
						// X2 = 0

						data[count++] = X1[numGroups - 1];
					}
					else if (missingValManagement == 1)
					{
						data[count++] = primaryMissingValue;
					}
				}
				else
				{
					X2 = bits2UInt(numBitsEncodingEachGroup[numGroups - 1], raf);
					if (missingValManagement == 0)
					{
						data[count++] = X1[numGroups - 1] + X2;
					}
					else if (missingValManagement == 1)
					{
						// X2 is also set to missing value is all bits set to 1's
						if (X2 == bitsmv1[numBitsEncodingEachGroup[numGroups - 1]])
						{
							data[count++] = primaryMissingValue;
						}
						else
						{
							data[count++] = X1[numGroups - 1] + X2;
						}
					}
				}
			} // end for j
			
			
			//System.out.println( "DS mvm =" + mvm );
			if (orderSpatial == 1)
			{
				// g1 and gMin this coding is a sort of guess, no doc
				float sum = 0;
				if (missingValManagement == 0)
				{
					// no missing values
					for (int i = 1; i < data.Length; i++)
					{
						data[i] += gMin; // add minimum back
					}
					data[0] = g1;
					for (int i = 1; i < data.Length; i++)
					{
						sum += data[i];
						data[i] = data[i - 1] + sum;
					}
				}
				else
				{
					// contains missing values
					float lastOne = primaryMissingValue;
					// add the minimum back and set g1
					int idx = 0;
					for (int i = 0; i < data.Length; i++)
					{
						if (data[i] != primaryMissingValue)
						{
							if (idx == 0)
							{
								// set g1
								data[i] = g1;
								lastOne = data[i];
								idx = i + 1;
							}
							else
							{
								data[i] += gMin;
							}
						}
					}
					//System.out.println( "DS data[ 0 ] ="+ data[ 0 ] );
					if (lastOne == primaryMissingValue)
					{
						System.Console.Out.WriteLine("DS bad spatial differencing data");
						return ;
					}
					for (int i = idx; i < data.Length; i++)
					{
						if (data[i] != primaryMissingValue)
						{
							//System.out.println( "DS pInvItem=" + pInvItem + " sum =" + sum );
							sum += data[i];
							data[i] = lastOne + sum;
							lastOne = data[i];
							//System.out.println( "DS data[ "+ pInvItem +" ] =" + data[ pInvItem ] );
						}
					}
				}
			}
			else if (orderSpatial == 2)
			{
				//h1, h2, hMin
				float hDiff = h2 - h1;
				float sum = 0;
				if (missingValManagement == 0)
				{
					// no missing values
					for (int i = 2; i < data.Length; i++)
					{
						data[i] += hMin; // add minimum back
					}
					data[0] = h1;
					data[1] = h2;
					sum = hDiff;
					for (int i = 2; i < data.Length; i++)
					{
						sum += data[i];
						data[i] = data[i - 1] + sum;
					}
				}
				else
				{
					// contains missing values
					int idx = 0;
					float lastOne = primaryMissingValue;
					// add the minimum back and set h1 and h2
					for (int i = 0; i < data.Length; i++)
					{
						if (data[i] != primaryMissingValue)
						{
							if (idx == 0)
							{
								// set h1
								data[i] = h1;
								sum = 0;
								lastOne = data[i];
								idx++;
							}
							else if (idx == 1)
							{
								// set h2
								data[i] = h1 + hDiff;
								sum = hDiff;
								lastOne = data[i];
								idx = i + 1;
							}
							else
							{
								data[i] += hMin;
							}
						}
					}
					if (lastOne == primaryMissingValue)
					{
						System.Console.Out.WriteLine("DS bad spatial differencing data");
						return ;
					}
					for (int i = idx; i < data.Length; i++)
					{
						if (data[i] != primaryMissingValue)
						{
							//System.out.println( "DS pInvItem=" + pInvItem + " sum =" + sum );
							sum += data[i];
							//System.out.println( "DS before data[ "+ pInvItem +" ] =" + data[ pInvItem ] );
							data[i] = lastOne + sum;
							lastOne = data[i];
							//System.out.println( "DS after data[ "+ pInvItem +" ] =" + data[ pInvItem ] );
						}
					}
				}
			} // end h1, h2, hMin
			
			// formula used to create values,  Y * 10**D = R + (X1 + X2) * 2**E
			
			int D = drs.DecimalScaleFactor;
			//System.out.println( "DS D=" + D );
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			float DD = (float) System.Math.Pow((double) 10, (double) D);
			//System.out.println( "DS DD=" + DD );
			
			float R = drs.ReferenceValue;
			//System.out.println( "DS R=" + R );
			
			int E = drs.BinaryScaleFactor;
			//System.out.println( "DS E=" + E );
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			float EE = (float) System.Math.Pow((double) 2.0, (double) E);
			//System.out.println( "DS EE=" + EE );
			
			if (missingValManagement == 0)
			{
				// no missing values
				for (int i = 0; i < data.Length; i++)
				{
					data[i] = (R + data[i] * EE) / DD;
				}
			}
			else
			{
				// missing value == 1
				for (int i = 0; i < data.Length; i++)
				{
					if (data[i] != primaryMissingValue)
					{
						data[i] = (R + data[i] * EE) / DD;
					}
				}
			}
			scanMode = gds.ScanMode;
			scanningModeCheck();
			//System.out.println( "DS true end =" + gribStream.position() );
		} // end complexUnpackingWithSpatial
		
		/// <summary> Jpeg2000 unpacking method for Grib2 data.</summary>
		/// <param gridTemplateName="gribStream">
		/// </param>
		/// <param gridTemplateName="gds">
		/// </param>
		/// <param gridTemplateName="drs">
		/// </param>
		/// <param gridTemplateName="bms">bit-map numberOfSection object
		/// </param>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		private void  jpeg2000Unpacking(System.IO.Stream raf, Grib2GridDefinitionSection gds, Grib2DataRepresentationSection drs, Grib2BitMapSection bms)
		{
            if (bms.BitMapIndicator != 255 && bms.BitMapIndicator != 0)
                throw new NotImplementedException("jpeg2000 unpacking with a bitmap indicator other than 0 or 255 is currently not supported");
			// 6-xx  jpeg2000 data block to decode
			
			// dataPoints are number of points encoded, it could be less than the
			// numberOfDataPoints in the grid record if bitMap is used, otherwise equal
			int dataPoints = drs.DataPoints;
			//System.out.println( "DS DRS dataPoints=" + drs.getDataPoints() );
			//System.out.println( "DS lengthOfSection=" + lengthOfSection );
			
			float pmv = drs.PrimaryMissingValue;
			//System.out.println( "DS pmv=" + pmv );
			int nb = drs.NumberOfBits;
			//System.out.println( "DS numberOfBits = " + numberOfBits );
			
			int D = drs.DecimalScaleFactor;
			//System.out.println( "DS D=" + D );
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			float DD = (float) System.Math.Pow((double) 10, (double) D);
			//System.out.println( "DS DD=" + DD );
			
			float R = drs.ReferenceValue;
			//System.out.println( "DS R=" + R );
			
			int E = drs.BinaryScaleFactor;
			//System.out.println( "DS E=" + E );
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			float EE = (float) System.Math.Pow((double) 2.0, (double) E);
			//System.out.println( "DS EE=" + EE );
			
			/*Grib2JpegDecoder g2j = null;
			if (numberOfBits != 0)
			{
				// there's data to decode
				System.String[] argv = new System.String[4];
				argv[0] = "-rate";
				argv[1] = System.Convert.ToString(numberOfBits);
				argv[2] = "-verbose";
				argv[3] = "off";
				//argv[ 2 ] = "-nocolorspace" ;
				//argv[ 3 ] = "-Rno_roi" ;
				//argv[ 4 ] = "-cdstr_info" ;
				//argv[ 5 ] = "-verbose" ;
				//argv[ 6 ] = "-debug" ;
				g2j = new Grib2JpegDecoder(argv);
				g2j.decode(gribStream, lengthOfSection - 5);
			}
             */

            Jpeg2000Decoder decoder = new Jpeg2000Decoder();
            byte[] buf = new byte[length-5];
            // TODO Check cast
            int res = raf.Read(buf, 0, length-5);
            int[] values = null;
            if( res == length-5 )
            {
                values = decoder.Decode(buf, dataPoints);
            }
            else
            {
                // TODO Error handling
            }

			int numberPoints = gds.NumberOfDataPoints;
			//System.out.println( "DS GDS NumberOfDataPoints=" +  gds.getNumberPoints() );
			data = new float[numberPoints];
			bool[] bitmap = bms.Bitmap;
			
			if (bitmap == null)
			{
				if (nb == 0)
				{
					// no data decoded, set to primaryMissingValue
					for (int i = 0; i < numberPoints; i++)
					{
						data[i] = pmv;
					}
				}
				else
				{
					//System.out.println( "DS jpeg data lengthOfSection ="+ g2j.data.lengthOfSection );
					// record has missing bitmap
					if (values.Length != numberPoints)
					{
						data = null;
						return ;
					}
					for (int i = 0; i < numberPoints; i++)
					{
						//Y = (R + ( 0 + X2) * EE)/DD ;
						data[i] = (R + values[i] * EE) / DD;
						//System.out.println( "DS data[ " + pInvItem +"  ]=" + data[ pInvItem ] );
					}
				}
			}
			else
			{
				for (int i = 0, j = 0; i < bitmap.Length; i++)
				{
					if (bitmap[i])
					{
						data[i] = (R + values[j++] * EE) / DD;
					}
					else
					{
						data[i] = pmv;
					}
				}
			}
			scanMode = gds.ScanMode;
			scanningModeCheck();
		} // end jpeg2000Unpacking
		
		
		/// <summary> Convert bits (numberOfBits) to Unsigned Int .</summary>
		/// <param gridTemplateName="numberOfBits">the number of bits to convert to int
		/// </param>
		/// <param gridTemplateName="gribStream">
		/// </param>
		/// <throws>  IOException </throws>
		/// <returns> int of DataSections numberOfSection
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
		} // end bits2UInt
		
		/// <summary> Rearrange the data array using the scanning mode.
		/// 
		/// </summary>
		private void  scanningModeCheck()
		{
			// Mode  0 +x, -y, adjacent x, adjacent rows same dir
			// Mode  64 +x, +y, adjacent x, adjacent rows same dir
			if (scanMode == 0 || scanMode == 64)
			{
				return ;
				// Mode  128 -x, -y, adjacent x, adjacent rows same dir
				// Mode  192 -x, +y, adjacent x, adjacent rows same dir
				// change -x to +x ie east to west -> west to east
			}
			else if (scanMode == 128 || scanMode == 192)
			{
				float tmp;
				int mid = (int) Xlength / 2;
				//System.out.println( "Xlength =" +Xlength +" mid ="+ mid );
				for (int index = 0; index < data.Length; index += Xlength)
				{
					for (int idx = 0; idx < mid; idx++)
					{
						tmp = data[index + idx];
						data[index + idx] = data[index + Xlength - idx - 1];
						data[index + Xlength - idx - 1] = tmp;
						//System.out.println( "switch " + (index + idx) + " " +
						//(index + Xlength -idx -1) );
					}
				}
				return ;
			}
			// else
			// scanMode == 16, 80, 144, 208 adjacent rows scan opposite dir
			float tmp2;
			int mid2 = (int) Xlength / 2;
			//System.out.println( "Xlength =" +Xlength +" mid ="+ mid );
			for (int index = 0; index < data.Length; index += Xlength)
			{
				int row = (int) index / Xlength;
				if (row % 2 == 1)
				{
					// odd numbered row, calculate reverse index
					for (int idx = 0; idx < mid2; idx++)
					{
						tmp2 = data[index + idx];
						data[index + idx] = data[index + Xlength - idx - 1];
						data[index + Xlength - idx - 1] = tmp2;
						//System.out.println( "switch " + (index + idx) + " " +
						//(index + Xlength -idx -1) );
					}
				}
			}
		} // end of scanningModeCheck
		
		// --Commented out by Inspection START (11/21/05 11:16 AM):
		//   /**
		//    * Get the byte lengthOfSection of the DataSectionS numberOfSection.
		//    *
		//    * @return lengthOfSection in bytes of DataSectionS numberOfSection
		//    */
		//   public final int getLength()
		//   {
		//      return lengthOfSection;
		//   }
		// --Commented out by Inspection STOP (11/21/05 11:16 AM)
		
		// --Commented out by Inspection START (11/21/05 11:16 AM):
		//   /**
		//    * Number of this numberOfSection, should be 7
		//    */
		//   public final int getSection()
		//   {
		//      return numberOfSection;
		//   }
		// --Commented out by Inspection STOP (11/21/05 11:16 AM)
	}
}