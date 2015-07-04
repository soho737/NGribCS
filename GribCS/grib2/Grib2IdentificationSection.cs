using NGribCS.Helpers;
/*
 * This file is part of NGribCS.
 * NGribCS is derived from GribCS
 * It is licensed on the GNU LGPL with the additional
 * restriction that this work may not be used for military or intelligence applications.
 * 
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
/// <summary> Grib2IdentificationSection.java  1.0  07/25/2003</summary>
/// <author>  Robb Kambic
/// 
/// </author>
using System;
using System.Runtime.InteropServices;

namespace NGribCS.Grib2
{
	
	
	/// <summary> A class representing the IdentificationSection section 1 of a GRIB record.
	/// 
	/// </summary>
    [GuidAttribute("5CFB4168-FB81-4c74-8700-6E8E2373E1AF")]
    [ClassInterface(ClassInterfaceType.None)]
	public sealed class Grib2IdentificationSection : NGribCS.Grib2.IGrib2IdentificationSection
	{
		/// <summary> Identification of center.</summary>
		/// <returns> center id as int
		/// </returns>
		public int Center_id
		{
			get
			{
				return center_id;
			}
			
		}
		/// <summary> Name of Identification of center.</summary>
		/// <returns> center Identification Name
		/// </returns>
		public System.String Center_idName
		{
			get
			{
				switch (center_id)
				{
					
					case 0:  return "WMO Secretariat";
					
					case 1: 
					case 2:  return "Melbourne";
					
					case 4: 
					case 5:  return "Moscow";
					
					case 7:  return "US National Weather Service (NCEP)";
					
					case 8:  return "US National Weather Service (NWSTG)";
					
					case 9:  return "US National Weather Service (other)";
					
					case 10:  return "Cairo (RSMC/RAFC)";
					
					case 12:  return "Dakar (RSMC/RAFC)";
					
					case 14:  return "Nairobi (RSMC/RAFC)";
					
					case 18:  return "Tunis Casablanca (RSMC)";
					
					case 20:  return "Las Palmas (RAFC)";
					
					case 21:  return "Algiers (RSMC)";
					
					case 24:  return "Pretoria (RSMC)";
					
					case 25:  return "La R?union (RSMC)";
					
					case 26:  return "Khabarovsk (RSMC)";
					
					case 28:  return "New Delhi (RSMC/RAFC)";
					
					case 30:  return "Novosibirsk (RSMC)";
					
					case 32:  return "Tashkent (RSMC)";
					
					case 33:  return "eddah (RSMC)";
					
					case 34:  return "Tokyo (RSMC), Japan Meteorological Agency";
					
					case 36:  return "Bangkok";
					
					case 37:  return "Ulan Bator";
					
					case 38:  return "Beijing (RSMC)";
					
					case 40:  return "Seoul";
					
					case 41:  return "Buenos Aires (RSMC/RAFC)";
					
					case 43:  return "Brasilia (RSMC/RAFC)";
					
					case 45:  return "Santiago";
					
					case 46:  return "Brazilian Space Agency ? INPE";
					
					case 51:  return "Miami (RSMC/RAFC)";
					
					case 52:  return "Miami RSMC, National Hurricane Center";
					
					case 53:  return "Montreal (RSMC)";
					
					case 55:  return "San Francisco";
					
					case 57:  return "Air Force Weather Agency";
					
					case 58:  return "Fleet Numerical Meteorology and Oceanography Center";
					
					case 59:  return "The NOAA Forecast Systems Laboratory";
					
					case 60:  return "United States National Centre for Atmospheric Research (NCAR)";
					
					case 64:  return "Honolulu";
					
					case 65:  return "Darwin (RSMC)";
					
					case 67:  return "Melbourne (RSMC)";
					
					case 69:  return "Wellington (RSMC/RAFC)";
					
					case 71:  return "Nadi (RSMC)";
					
					case 74:  return "UK Meteorological Office Bracknell (RSMC)";
					
					case 76:  return "Moscow (RSMC/RAFC)";
					
					case 78:  return "Offenbach (RSMC)";
					
					case 80:  return "Rome (RSMC)";
					
					case 82:  return "Norrk?ping";
					
					case 85:  return "Toulouse (RSMC)";
					
					case 86:  return "Helsinki";
					
					case 87:  return "Belgrade";
					
					case 88:  return "Oslo";
					
					case 89:  return "Prague";
					
					case 90:  return "Episkopi";
					
					case 91:  return "Ankara";
					
					case 92:  return "Frankfurt/Main (RAFC)";
					
					case 93:  return "London (WAFC)";
					
					case 94:  return "Copenhagen";
					
					case 95:  return "Rota";
					
					case 96:  return "Athens";
					
					case 97:  return "European Space Agency (ESA)";
					
					case 98:  return "ECMWF, RSMC";
					
					case 99:  return "De Bilt";
					
					case 110:  return "Hong-Kong";
					
					case 210:  return "Frascati (ESA/ESRIN)";
					
					case 211:  return "Lanion";
					
					case 212:  return "Lisboa";
					
					case 213:  return "Reykjavik";
					
					case 254:  return "EUMETSAT Operation Centre";
					
					
					default:  return "Unknown";
					
				}
			}
			
		}
		/// <summary> Identification of subcenter.</summary>
		/// <returns> subcenter as int
		/// </returns>
		public int Subcenter_id
		{
			get
			{
				return subcenter_id;
			}
			
		}
		/// <summary> Parameter Table Version number.</summary>
		/// <returns>  master_table_version as int
		/// </returns>
		public int Master_table_version
		{
			get
			{
				return master_table_version;
			}
			
		}
		/// <summary> local table version number.</summary>
		/// <returns> local_table_version as int
		/// </returns>
		public int Local_table_version
		{
			get
			{
				return local_table_version;
			}
			
		}
		/// <summary> Model Run/Analysis/Reference time.</summary>
		/// <returns> significanceOfRT as int
		/// </returns>
		public int SignificanceOfRT
		{
			get
			{
				return significanceOfRT;
			}
			
		}
		/// <summary> Model Run/Analysis/Reference time.</summary>
		/// <returns> significanceOfRT Name
		/// </returns>
		public System.String SignificanceOfRTName
		{
			get
			{
				switch (significanceOfRT)
				{
					
					case 0:  return "Analysis";
					
					case 1:  return "Start of forecast";
					
					case 2:  return "Verifying time of forecast";
					
					case 3:  return "Observation time";
					
					
					default:  return "Unknown";
					
				}
			}
			
		}
		/// <summary> return reference time of product.</summary>
		/// <returns> referenceTime
		/// </returns>
		public System.String ReferenceTime
		{
			get
			{
				return referenceTime.ToString();
			}
			
		}
		/// <summary> reference time as Calendar.</summary>
		/// <returns> baseTime
		/// </returns>
		public System.DateTime BaseTime
		{
			get
			{
				return baseTime;
			}
			
		}
		/// <summary> productStatus
		/// values are operational, test, research, etc.
		/// </summary>
		/// <returns> productStatus as int
		/// </returns>
		public int ProductStatus
		{
			get
			{
				return productStatus;
			}
			
		}
		/// <summary> productStatusName.</summary>
		/// <returns> productStatus name
		/// </returns>
		public System.String ProductStatusName
		{
			get
			{
				switch (productStatus)
				{
					
					case 0:  return "Operational products";
					
					case 1:  return "Operational test products";
					
					case 2:  return "Research products";
					
					case 3:  return "Re-analysis products";
					
					
					default:  return "Unknown";
					
				}
			}
			
		}
		/// <summary> Product type.</summary>
		/// <returns> productType as int
		/// </returns>
		public int ProductType
		{
			get
			{
				return productType;
			}
			
		}
		/// <summary> Product type name.</summary>
		/// <returns> productType name
		/// </returns>
		public System.String ProductTypeName
		{
			get
			{
				switch (productType)
				{
					
					case 0:  return "Analysis products";
					
					case 1:  return "Forecast products";
					
					case 2:  return "Analysis and forecast products";
					
					case 3:  return "Control forecast products";
					
					case 4:  return "Perturbed forecast products";
					
					case 5:  return "Control and Perturbed forecast products";
					
					case 6:  return "Processed satellite observations";
					
					case 7:  return "Processed radar observations";
					
					
					default:  return "Unknown";
					
				}
			}
			
		}

        /// <summary> Reference time as System.DateTime.</summary>
        public System.DateTime RefTime
        {
            get
            {
                return refTime;
            }
        }

        /// <summary> Reference time as C type time_t.</summary>
        public int RefTimeT
        {
            get
            {
                DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan rt = refTime - startTime;
                int t = Convert.ToInt32(rt.TotalSeconds);
                return t;
            }
        }

		private static System.Globalization.DateTimeFormatInfo dateFormat;
		
		/// <summary> Length in bytes of this IdentificationSection.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'length '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int length;
		
		/// <summary> Number of this section, should be 1.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'section '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int section;
		
		/// <summary> Identification of center.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'center_id '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int center_id;
		
		/// <summary> Identification of subcenter .</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'subcenter_id '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int subcenter_id;
		
		/// <summary> Parameter Table Version number.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'master_table_version '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int master_table_version;
		//UPGRADE_NOTE: Final was removed from the declaration of 'local_table_version '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int local_table_version;
		
		/// <summary> Model Run/Analysis/Reference time.
		/// 
		/// </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'significanceOfRT '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int significanceOfRT;
		//UPGRADE_NOTE: Final was removed from the declaration of 'referenceTime '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String referenceTime;

        private System.DateTime refTime;
		//UPGRADE_NOTE: Final was removed from the declaration of 'baseTime '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        // TODO Why baseTime???
        private System.DateTime baseTime;
		
		/// <summary> Product status. operational, test, research, etc.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'productStatus '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int productStatus;
		
		/// <summary> Product type. forecast, analysis, etc.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'productType '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int productType;
		
		// *** constructors *******************************************************
		
		/// <summary> Constructs a <tt>Grib2IdentificationSection</tt> object from a RandomAccessFile.
		/// 
		/// </summary>
		/// <param name="raf">RandomAccessFile with Section 1 content
		/// 
		/// </param>
		/// <throws>  IOException  if raf contains no valid GRIB file </throws>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public Grib2IdentificationSection(System.IO.Stream raf)
		{
			// section 1 octet 1-4 (length of section)
			length = GribNumbers.int4(raf);
			
			//System.out.println( "IdentificationSection1 length=" + length );
			
			section = raf.ReadByte();
			//System.out.println( "Section number=" + section );
			
			// Center  octet 6-7
			center_id = GribNumbers.int2(raf);
			//System.out.println( "center_id=" + center_id );
			
			// Center  octet 8-9
			subcenter_id = GribNumbers.int2(raf);
			//System.out.println( "subcenter_id=" + subcenter_id );
			
			// Paramter master table octet 10
			master_table_version = raf.ReadByte();
			//System.out.println( "master tbl=" + master_table_version );
			
			// Paramter local table octet 11
			local_table_version = raf.ReadByte();
			//System.out.println( "local tbl=" + local_table_version );
			
			// significanceOfRT octet 12
			significanceOfRT = raf.ReadByte();
			//System.out.println( "significanceOfRT=" + significanceOfRT );
			
			// octets 13-19 (base time of forecast)
			{
				int year = GribNumbers.int2(raf);
				int month = raf.ReadByte();
				int day = raf.ReadByte();
				int hour = raf.ReadByte();
				int minute = raf.ReadByte();
				int second = raf.ReadByte();

                refTime = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
                baseTime = refTime;
                referenceTime = refTime.ToString(dateFormat);
			}
			
			productStatus = raf.ReadByte();
			//System.out.println( "productStatus=" + productStatus );
			
			productType = raf.ReadByte();
			//System.out.println( "productType=" + productType );
		} // end if Grib2IdentificationSection
		
		
		// --Commented out by Inspection START (11/21/05 12:42 PM):
		//   /**
		//    * Get the byte length of this section.
		//    *
		//    * @return length in bytes of this section
		//    */
		//   public final int getLength()
		//   {
		//      return length;
		//   }
		// --Commented out by Inspection STOP (11/21/05 12:42 PM)
		
		// --Commented out by Inspection START (11/21/05 12:42 PM):
		//   /**
		//    * Number of this section, should be 1
		//    */
		//   public final int getSection()
		//   {
		//      return section;
		//   }
		// --Commented out by Inspection STOP (11/21/05 12:42 PM)
		static Grib2IdentificationSection()
		{
			{
				//UPGRADE_ISSUE: Constructor 'java.text.SimpleDateFormat.SimpleDateFormat' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javatextSimpleDateFormat'"
				dateFormat = new System.Globalization.DateTimeFormatInfo();
			}
		}
	}
}