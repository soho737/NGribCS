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
	
	
	/// <summary> A class that represents the DataRepresentationSection of a GRIB product.
	/// 
	/// </summary>
    [GuidAttribute("F9E626F2-0E00-445c-938F-7444E98C947A")]
    [ClassInterface(ClassInterfaceType.None)]
    public sealed class Grib2DataRepresentationSection : NGribCS.Grib2.IGrib2DataRepresentationSection
	{
		private void  InitBlock()
		{
			primaryMissingValue = GribNumbers.UNDEFINED;
			secondaryMissingValue = GribNumbers.UNDEFINED;
		}
		/// <summary> Get the byte lengthOfSection of the Section DRS numberOfSection.
		/// 
		/// </summary>
		/// <returns> lengthOfSection in bytes of Section DRS numberOfSection
		/// </returns>
		public int Length
		{
			get
			{
				return length;
			}
			
		}
		/// <summary> Get the number of dataPoints in DS numberOfSection.
		/// 
		/// </summary>
		/// <returns> number of dataPoints in DS numberOfSection
		/// </returns>
		public int DataPoints
		{
			get
			{
				return dataPoints;
			}
			
		}
		/// <summary> Get the Data Template Number for the GRID.
		/// 
		/// </summary>
		/// <returns> Data Template Number
		/// </returns>
		public int DataTemplateNumber
		{
			get
			{
				return dataTemplate;
			}
			
		}
		/// <summary> Reference value (R) (IEEE 32-bit floating-point value).</summary>
		/// <returns> ReferenceValue
		/// </returns>
		public float ReferenceValue
		{
			get
			{
				return referenceValue;
			}
			
		}
		/// <summary> Binary scale factor (E).</summary>
		/// <returns> BinaryScaleFactor
		/// </returns>
		public int BinaryScaleFactor
		{
			get
			{
				return binaryScaleFactor;
			}
			
		}
		/// <summary> Decimal scale factor (D).</summary>
		/// <returns> DecimalScaleFactor
		/// </returns>
		public int DecimalScaleFactor
		{
			get
			{
				return decimalScaleFactor;
			}
			
		}
		/// <summary> Number of bits used for each packed value..</summary>
		/// <returns> NumberOfBits numBitsEncodingEachGroup
		/// </returns>
		public int NumberOfBits
		{
			get
			{
				return numberOfBits;
			}
			
		}
		/// <summary> Type of original field values.</summary>
		/// <returns> OriginalType dataType
		/// </returns>
		public int OriginalType
		{
			get
			{
				return originalType;
			}
			
		}
		/// <summary> Group splitting method used (see Code Table 5.4).</summary>
		/// <returns> SplittingMethod
		/// </returns>
		public int SplittingMethod
		{
			get
			{
				return splittingMethod;
			}
			
		}
		/// <summary> Type compression method used (see Code Table 5.40000).</summary>
		/// <returns> CompressionMethod
		/// </returns>
		public int CompressionMethod
		{
			get
			{
				return compressionMethod;
			}
			
		}
		/// <summary> Compression ratio used .</summary>
		/// <returns> CompressionRatio
		/// </returns>
		public int CompressionRatio
		{
			get
			{
				return compressionRatio;
			}
			
		}
		/// <summary> Missing value management used (see Code Table 5.5).</summary>
		/// <returns> MissingValueManagement
		/// </returns>
		public int MissingValueManagement
		{
			get
			{
				return missingValueManagement;
			}
			
		}
		/// <summary> Primary missing value substitute.</summary>
		/// <returns> PrimaryMissingValue
		/// </returns>
		public float PrimaryMissingValue
		{
			get
			{
				return primaryMissingValue;
			}
			
		}
		/// <summary> Secondary missing value substitute.</summary>
		/// <returns> SecondaryMissingValue
		/// </returns>
		public float SecondaryMissingValue
		{
			get
			{
				return secondaryMissingValue;
			}
			
		}
		/// <summary> numGroups - Number of groups of data values into which field is split.</summary>
		/// <returns> NumberOfGroups numGroups
		/// </returns>
		public int NumberOfGroups
		{
			get
			{
				return numberOfGroups;
			}
			
		}
		/// <summary> Reference for group widths (see Note 12).</summary>
		/// <returns> ReferenceGroupWidths
		/// </returns>
		public int ReferenceGroupWidths
		{
			get
			{
				return referenceGroupWidths;
			}
			
		}
		/// <summary> Number of bits used for the group widths (after the reference value 
		/// in octet 36 has been removed).
		/// </summary>
		/// <returns> BitsGroupWidths
		/// </returns>
		public int BitsGroupWidths
		{
			get
			{
				return bitsGroupWidths;
			}
			
		}
		/// <summary> Reference for group lengths (see Note 13).</summary>
		/// <returns> ReferenceGroupLength
		/// </returns>
		public int ReferenceGroupLength
		{
			get
			{
				return referenceGroupLength;
			}
			
		}
		/// <summary> Length increment for the group lengths (see Note 14).</summary>
		/// <returns> LengthIncrement
		/// </returns>
		public int LengthIncrement
		{
			get
			{
				return lengthIncrement;
			}
			
		}
		/// <summary> Length increment for the group lengths (see Note 14).</summary>
		/// <returns> LengthLastGroup
		/// </returns>
		public int LengthLastGroup
		{
			get
			{
				return lengthLastGroup;
			}
			
		}
		/// <summary> Number of bits used for the scaled group lengths (after subtraction of 
		/// the reference value given in octets 38-41 and division by the lengthOfSection 
		/// increment given in octet 42).
		/// </summary>
		/// <returns> BitsScaledGroupLength
		/// </returns>
		public int BitsScaledGroupLength
		{
			get
			{
				return bitsScaledGroupLength;
			}
			
		}
		/// <summary> Order of spatial differencing (see Code Table 5.6).</summary>
		/// <returns> OrderSpatial
		/// </returns>
		public int OrderSpatial
		{
			get
			{
				return orderSpatial;
			}
			
		}
		/// <summary> Number of octets required in the Data Section to specify extra
		/// descriptors needed for spatial differencing (octets 6-ww in Data
		/// Template 7.3).
		/// </summary>
		/// <returns> DescriptorSpatial
		/// </returns>
		public int DescriptorSpatial
		{
			get
			{
				return descriptorSpatial;
			}
			
		}
		
		/// <summary> Length in bytes of DataRepresentationSection numberOfSection.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'lengthOfSection '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int length;
		
		/// <summary> Number of this numberOfSection, should be 5.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'numberOfSection '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int section;
		
		/// <summary> Number of Data points.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'dataPoints '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int dataPoints;
		
		/// <summary> Data representation template number.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'dataTemplate '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int dataTemplate;
		
		/// <summary> Reference value (R) (IEEE 32-bit floating-point value).</summary>
		private float referenceValue;
		
		/// <summary> Binary scale factor (E).</summary>
		private int binaryScaleFactor;
		
		/// <summary> Decimal scale factor (D).</summary>
		private int decimalScaleFactor;
		
		/// <summary> Number of bits used for each packed value.</summary>
		private int numberOfBits;
		
		/// <summary> data type of original field values.</summary>
		private int originalType;
		
		/// <summary> Group splitting method used (see Code Table 5.4).</summary>
		private int splittingMethod;
		
		/// <summary> Type compression method used (see Code Table 5.40000).</summary>
		private int compressionMethod;
		
		/// <summary> Compression ratio used.</summary>
		private int compressionRatio;
		
		/// <summary> Missing value management used (see Code Table 5.5).</summary>
		private int missingValueManagement;
		
		/// <summary> Primary missing value substitute.</summary>
		//UPGRADE_NOTE: The initialization of  'primaryMissingValue' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private float primaryMissingValue;
		
		/// <summary> Secondary missing value substitute.</summary>
		//UPGRADE_NOTE: The initialization of  'secondaryMissingValue' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private float secondaryMissingValue;
		
		/// <summary> numGroups - Number of groups of data values into which field is split.</summary>
		private int numberOfGroups;
		
		/// <summary> Reference for group widths (see Note 12).</summary>
		private int referenceGroupWidths;
		
		/// <summary> Number of bits used for the group widths (after the reference value.
		/// in octet 36 has been removed)
		/// </summary>
		private int bitsGroupWidths;
		
		/// <summary> Reference for group lengths (see Note 13).</summary>
		private int referenceGroupLength;
		
		/// <summary> Length increment for the group lengths (see Note 14).</summary>
		private int lengthIncrement;
		
		/// <summary> Length increment for the group lengths (see Note 14).</summary>
		private int lengthLastGroup;
		
		/// <summary> Number of bits used for the scaled group lengths (after subtraction of 
		/// the reference value given in octets 38-41 and division by the lengthOfSection 
		/// increment given in octet 42).
		/// </summary>
		private int bitsScaledGroupLength;
		
		/// <summary> Order of spatial differencing (see Code Table 5.6).</summary>
		private int orderSpatial;
		
		/// <summary> Number of octets required in the Data Section to specify extra
		/// descriptors needed for spatial differencing (octets 6-ww in Data
		/// Template 7.3) .
		/// </summary>
		private int descriptorSpatial;
		
		// *** constructors *******************************************************
		
		/// <summary> Constructs a <tt>Grib2DataRepresentationSection</tt> object from a gribStream.
		/// 
		/// </summary>
		/// <param gridTemplateName="gribStream">RandomAccessFile with Section DRS content
		/// </param>
		/// <throws>  IOException  if stream contains no valid GRIB file </throws>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public Grib2DataRepresentationSection(System.IO.Stream raf)
		{
			InitBlock();
			// octets 1-4 (Length of DRS)
			length = GribNumbers.int4(raf);
			//System.out.println( "DRS lengthOfSection=" + lengthOfSection );
			
			section = raf.ReadByte();
			//System.out.println( "DRS is 5 numberOfSection=" + numberOfSection );
			
			dataPoints = GribNumbers.int4(raf);
			//System.out.println( "DRS dataPoints=" + dataPoints );
			
			dataTemplate = (int)GribNumbers.uint2(raf);
			//System.out.println( "DRS dataTemplate=" + dataTemplate );
			
			switch (dataTemplate)
			{
				
				// Data Template Number
				case 0: 
				case 1:  // 0 - Grid point data - simple packing 
					// 1 - Matrix values - simple packing
					//System.out.println( "DRS dataTemplate=" + dataTemplate );
                    referenceValue = GribNumbers.IEEEfloat4(raf);
					binaryScaleFactor = GribNumbers.int2(raf);
					decimalScaleFactor = GribNumbers.int2(raf);
					numberOfBits = raf.ReadByte();
					//System.out.println( "DRS numberOfBits=" + numberOfBits );
					originalType = raf.ReadByte();
					//System.out.println( "DRS originalType=" + originalType );
					
					if (dataTemplate == 0)
						break;
					// case 1 not implememted
					System.Console.Out.WriteLine("DRS dataTemplate=1 not implemented yet");
					break;
				
				case 2: 
				case 3:  // Grid point data - complex packing
					//System.out.println( "DRS dataTemplate=" + dataTemplate );
					// octet 12 - 15
                    referenceValue = GribNumbers.IEEEfloat4(raf);
					// octet 16 - 17
					binaryScaleFactor = GribNumbers.int2(raf);
					// octet 18 - 19
					decimalScaleFactor = GribNumbers.int2(raf);
					// octet 20
					numberOfBits = raf.ReadByte();
					//System.out.println( "DRS numberOfBits=" + numberOfBits );
					// octet 21
					originalType = raf.ReadByte();
					//System.out.println( "DRS originalType=" + originalType );
					// octet 22
					splittingMethod = raf.ReadByte();
					//System.out.println( "DRS splittingMethod=" + 
					//     splittingMethod );
					// octet 23
					missingValueManagement = raf.ReadByte();
					//System.out.println( "DRS missingValueManagement=" + 
					//     missingValueManagement );
					// octet 24 - 27
                    primaryMissingValue = GribNumbers.IEEEfloat4(raf);
					// octet 28 - 31
                    secondaryMissingValue = GribNumbers.IEEEfloat4(raf);
					// octet 32 - 35
					numberOfGroups = GribNumbers.int4(raf);
					//System.out.println( "DRS numberOfGroups=" + 
					//     numberOfGroups );
					// octet 36
					referenceGroupWidths = raf.ReadByte();
					//System.out.println( "DRS referenceGroupWidths=" + 
					//     referenceGroupWidths );
					// octet 37
					bitsGroupWidths = raf.ReadByte();
					// according to documentation subtract referenceGroupWidths
					bitsGroupWidths = bitsGroupWidths - referenceGroupWidths;
					//System.out.println( "DRS bitsGroupWidths=" + 
					//     bitsGroupWidths );
					// octet 38 - 41
					referenceGroupLength = GribNumbers.int4(raf);
					//System.out.println( "DRS referenceGroupLength=" + 
					//     referenceGroupLength );
					// octet 42
					lengthIncrement = raf.ReadByte();
					//System.out.println( "DRS lengthIncrement=" + 
					//     lengthIncrement );
					// octet 43 - 46
					lengthLastGroup = GribNumbers.int4(raf);
					//System.out.println( "DRS lengthLastGroup=" + 
					//     lengthLastGroup );
					// octet 47
					bitsScaledGroupLength = raf.ReadByte();
					//System.out.println( "DRS bitsScaledGroupLength=" + 
					//     bitsScaledGroupLength );
					if (dataTemplate == 2)
						break;
					
					// case 3 // complex packing & spatial differencing
					orderSpatial = raf.ReadByte();
					//System.out.println( "DRS orderSpatial=" + orderSpatial);
					descriptorSpatial = raf.ReadByte();
					//System.out.println( "DRS descriptorSpatial=" + descriptorSpatial);
					break;
				
				
				case 40: 
				case 40000:  // Grid point data - JPEG 2000 Code Stream Format
					//System.out.println( "DRS dataTemplate=" + dataTemplate );

                    referenceValue = GribNumbers.IEEEfloat4(raf);
					binaryScaleFactor = GribNumbers.int2(raf);
					decimalScaleFactor = GribNumbers.int2(raf);
					numberOfBits = raf.ReadByte();
					//System.out.println( "DRS numberOfBits=" + numberOfBits );
					originalType = raf.ReadByte();
					//System.out.println( "DRS originalType=" + originalType );
					compressionMethod = raf.ReadByte();
					//System.out.println( "DRS compressionMethod=" + compressionMethod );
					compressionRatio = raf.ReadByte();
					//System.out.println( "DRS compressionRatio=" + compressionRatio );
					break;
				
				default: 
					break;
				
			}
		} // end of Grib2DataRepresentationSection
	}
}