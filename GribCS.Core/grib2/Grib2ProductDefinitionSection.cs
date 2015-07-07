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

using NGribCS;
using NGribCS.grib2;
using NGribCS.GribCS.grib2.Tables;
using NGribCS.Helpers;
/// <summary> Grib2ProductDefinitionSection.java  1.1  08/29/2003.</summary>
/// <author>  Robb Kambic
/// </author>
using System;
using System.Runtime.InteropServices;

namespace NGribCS.Grib2
{

    public enum TimeRangeUnits { Hour = 1, Day = 2, Month = 3, Year = 4, Decade = 5, Normal = 6, Century = 7, ThreeHours = 10, SixHours=11, TwelveHours=12, Seconds = 13};


	/// <summary> A class representing the product definition section (PDS) of a GRIB product.</summary>
    [GuidAttribute("BE2D595F-73CE-4600-8FD5-46A6F50D27A4")]
    [ClassInterface(ClassInterfaceType.None)]
    public sealed class Grib2ProductDefinitionSection : NGribCS.Grib2.IGrib2ProductDefinitionSection
    {
        /// <summary> Number of this coordinates.</summary>
        /// <returns>  Coordinates number
        /// </returns>
        public int Coordinates
        {
            get
            {
                return coordinates;
            }

        }
        /// <summary> productDefinition.</summary>
        /// <returns> ProductDefinition
        /// </returns>
        public int ProductDefinition
        {
            get
            {
                return productDefinition;
            }

        }
        /// <summary> parameter Category .</summary>
        /// <returns> parameterCategory as int
        /// </returns>
        public int ParameterCategory
        {
            get
            {
                return parameterCategoryId;
            }

        }
        /// <summary> parameter Number.</summary>
        /// <returns> ParameterNumber
        /// </returns>
        public int ParameterNumber
        {
            get
            {
                return parameterNumber;
            }

        }
        /// <summary> typeGenProcess.</summary>
        /// <returns> GenProcess
        /// </returns>
        public int TypeGenProcess
        {
            get
            {
                return typeGenProcess;
            }

        }
        /// <summary> backGenProcess.</summary>
        /// <returns> BackGenProcess
        /// </returns>
        public int BackGenProcess
        {
            get
            {
                return backGenProcess;
            }

        }
        /// <summary> analysisGenProcess.</summary>
        /// <returns> analysisGenProcess
        /// </returns>
        public int AnalysisGenProcess
        {
            get
            {
                return analysisGenProcess;
            }

        }
        /// <summary> hoursAfter.</summary>
        /// <returns> HoursAfter
        /// </returns>
        public int HoursAfter
        {
            get
            {
                return hoursAfter;
            }

        }
        /// <summary> minutesAfter.</summary>
        /// <returns>  MinutesAfter
        /// </returns>
        public int MinutesAfter
        {
            get
            {
                return minutesAfter;
            }

        }
        /// <summary> returns timeRangeUnitCode .</summary>
        /// <returns> TimeRangeUnitName
        /// </returns>
        public int TimeRangeUnitCode
        {
            get
            {
                return timeRangeUnitCode;
            }

        }

        public TimeRangeUnits TimeRangeUnit
        {
            get
            {
                return (TimeRangeUnits)timeRangeUnitCode;
            }
        }
        /// <summary> forecastTime.</summary>
        /// <returns> ForecastTime
        /// </returns>
        public int ForecastTime
        {
            get
            {
                return forecastTime;
            }

        }

        public Grib2SurfaceDefinition SurfaceDefinition
        {
            get
            {
                return null;
            }
        }

     

        /// <summary> Length in bytes of this PDS.</summary>
        //UPGRADE_NOTE: Final was removed from the declaration of 'length '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private int length;

        /// <summary> Number of this section, should be 4.</summary>
        //UPGRADE_NOTE: Final was removed from the declaration of 'section '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private int section;

        /// <summary> Number of this coordinates.</summary>
        //UPGRADE_NOTE: Final was removed from the declaration of 'coordinates '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private int coordinates;

        /// <summary> productDefinition.</summary>
        //UPGRADE_NOTE: Final was removed from the declaration of 'productDefinition '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private int productDefinition;

        /// <summary> parameterCategory.</summary>
        private int parameterCategoryId;


        /// <summary> parameterNumber.</summary>
        private int parameterNumber;

        /// <summary> typeGenProcess.</summary>
        private int typeGenProcess;

        /// <summary> backGenProcess.</summary>
        private int backGenProcess;

        /// <summary> analysisGenProcess.</summary>
        private int analysisGenProcess;

        /// <summary> hoursAfter.</summary>
        private int hoursAfter;

        /// <summary> minutesAfter.</summary>
        private int minutesAfter;

        /// <summary> timeRangeUnitCode.</summary>
        internal int timeRangeUnitCode;

        /// <summary> forecastTime.</summary>
        private int forecastTime;

        /// <summary> typeFirstFixedSurface.</summary>
        private int typeFirstFixedSurface;

        /// <summary> value of FirstFixedSurface.
        /// 
        /// </summary>
        private float firstFixedSurfaceValue;

        /// <summary> typeSecondFixedSurface.</summary>
        private int typeSecondFixedSurface;

        /// <summary> SecondFixedSurface Value.</summary>
        private float secondFixedSurfaceValue;

        /// <summary>  number of bands.</summary>
        private int nb;

        // *** constructors *******************************************************


        /// <summary> Constructs a Grib2ProductDefinitionSection  object from a raf.
        /// 
        /// </summary>
        /// <param name="raf">RandomAccessFile with PDS content
        /// 
        /// </param>
        /// <throws>  IOException  if raf contains no valid GRIB file </throws>
        //UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
        public Grib2ProductDefinitionSection(System.IO.Stream raf, int pCenterId = 0, int pMasterTableVersion = 0, int pLocalTableVersion = 0)
        {
            // octets 1-4 (Length of PDS)
            length = GribNumbers.int4(raf);
            //System.out.println( "PDS length=" + length );

            // octet 5
            section = raf.ReadByte();
            //System.out.println( "PDS is 4, section=" + section );

            // octet 6-7
            coordinates = GribNumbers.int2(raf);
            //System.out.println( "PDS coordinates=" + coordinates );

            // octet 8-9
            productDefinition = GribNumbers.int2(raf);
            //System.out.println( "PDS productDefinition=" + productDefinition );

            switch (productDefinition)
            {


                // Analysis or forecast at a horizontal level or in a horizontal
                // layer at a point in time
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    {

                        // octet 10
                        parameterCategoryId = raf.ReadByte();
                        //System.out.println( "PDS parameterCategory=" + 
                        //parameterCategory );

                        // octet 11
                        parameterNumber = raf.ReadByte();
                        //System.out.println( "PDS parameterNumber=" + parameterNumber );

                        // octet 12
                        typeGenProcess = raf.ReadByte();
                        //System.out.println( "PDS typeGenProcess=" + typeGenProcess );

                        // octet 13
                        backGenProcess = raf.ReadByte();
                        //System.out.println( "PDS backGenProcess=" + backGenProcess );

                        // octet 14
                        analysisGenProcess = raf.ReadByte();
                        //System.out.println( "PDS analysisGenProcess=" + 
                        //analysisGenProcess );

                        // octet 15-16
                        hoursAfter = GribNumbers.int2(raf);
                        //System.out.println( "PDS hoursAfter=" + hoursAfter );

                        // octet 17
                        minutesAfter = raf.ReadByte();
                        //System.out.println( "PDS minutesAfter=" + minutesAfter );

                        // octet 18
                        timeRangeUnitCode = raf.ReadByte();
                        //System.out.println( "PDS timeRangeUnitCode=" + timeRangeUnitCode );

                        // octet 19-22
                        forecastTime = GribNumbers.int4(raf);
                        //System.out.println( "PDS forecastTime=" + forecastTime );

                        // octet 23
                        typeFirstFixedSurface = raf.ReadByte();
                        //System.out.println( "PDS typeFirstFixedSurface=" + 
                        //     typeFirstFixedSurface );

                        // octet 24
                        int scaleFirstFixedSurface = raf.ReadByte();
                        //System.out.println( "PDS scaleFirstFixedSurface=" + 
                        //     scaleFirstFixedSurface );

                        // octet 25-28
                        int valueFirstFixedSurface = GribNumbers.int4(raf);
                        //System.out.println( "PDS valueFirstFixedSurface=" + 
                        //     valueFirstFixedSurface );

                        //UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
                        firstFixedSurfaceValue = (float)((scaleFirstFixedSurface == 0 || valueFirstFixedSurface == 0) ? valueFirstFixedSurface : System.Math.Pow(valueFirstFixedSurface, -scaleFirstFixedSurface));

                        // octet 29
                        typeSecondFixedSurface = raf.ReadByte();
                        //System.out.println( "PDS typeSecondFixedSurface=" + 
                        //typeSecondFixedSurface );

                        // octet 30
                        int scaleSecondFixedSurface = raf.ReadByte();
                        //System.out.println( "PDS scaleSecondFixedSurface=" + 
                        //scaleSecondFixedSurface );

                        // octet 31-34
                        int valueSecondFixedSurface = GribNumbers.int4(raf);
                        //System.out.println( "PDS valueSecondFixedSurface=" + 
                        //valueSecondFixedSurface );

                        //UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
                        secondFixedSurfaceValue = (float)((scaleSecondFixedSurface == 0 || valueSecondFixedSurface == 0) ? valueSecondFixedSurface : System.Math.Pow(valueSecondFixedSurface, -scaleSecondFixedSurface));


                        // Individual ensemble forecast, control and perturbed, at a
                        // horizontal level or in a horizontal layer at a point in time
                        if (productDefinition == 1)
                        {
                            System.Console.Out.WriteLine("PDS productDefinition == 1 not done");

                            //Derived forecast based on all ensemble members at a horizontal 
                            // level or in a horizontal layer at a point in time
                        }
                        else if (productDefinition == 2)
                        {
                            System.Console.Out.WriteLine("PDS productDefinition == 2 not done");

                            // Derived forecasts based on a cluster of ensemble members over
                            // a rectangular area at a horizontal level or in a horizontal layer
                            // at a point in time
                        }
                        else if (productDefinition == 3)
                        {
                            System.Console.Out.WriteLine("PDS productDefinition == 3 not done");

                            // Derived forecasts based on a cluster of ensemble members
                            // over a circular area at a horizontal level or in a horizontal
                            // layer at a point in time
                        }
                        else if (productDefinition == 4)
                        {
                            System.Console.Out.WriteLine("PDS productDefinition == 4 not done");

                            // Probability forecasts at a horizontal level or in a horizontal 
                            //  layer at a point in time
                        }
                        else if (productDefinition == 5)
                        {
                            System.Console.Out.WriteLine("PDS productDefinition == 5 not done");

                            // Percentile forecasts at a horizontal level or in a horizontal layer
                            // at a point in time
                        }
                        else if (productDefinition == 6)
                        {
                            System.Console.Out.WriteLine("PDS productDefinition == 6 not done");

                            // Analysis or forecast error at a horizontal level or in a horizontal 
                            // layer at a point in time
                        }
                        else if (productDefinition == 7)
                        {
                            System.Console.Out.WriteLine("PDS productDefinition == 7 not done");

                            // Average, accumulation, and/or extreme values at a horizontal
                            // level or in a horizontal layer in a continuous or non-continuous
                            // time interval
                        }
                        else if (productDefinition == 8)
                        {
                            //System.out.println( "PDS productDefinition == 8 " );
                            //  35-41 bytes
                            int year = GribNumbers.int2(raf);
                            int month = (raf.ReadByte()) - 1;
                            int day = raf.ReadByte();
                            int hour = raf.ReadByte();
                            int minute = raf.ReadByte();
                            int second = raf.ReadByte();
                            //System.out.println( "PDS date:" + year +":" + month +
                            //":" + day + ":" + hour +":" + minute +":" + second );

                            // 42 - 46
                            int timeRanges = raf.ReadByte();
                            //System.out.println( "PDS timeRanges=" + timeRanges ) ;
                            int missingDataValues = GribNumbers.int4(raf);
                            //System.out.println( "PDS missingDataValues=" + missingDataValues ) ;
                            // 47 - 48
                            int outmostTimeRange = raf.ReadByte();
                            //System.out.println( "PDS outmostTimeRange=" + outmostTimeRange )
                            ;
                            int missing = raf.ReadByte();
                            //System.out.println( "PDS missing=" + missing ) ;
                            // 49 - 53
                            int statisticalProcess = raf.ReadByte();
                            //System.out.println( "PDS statisticalProcess=" + statisticalProcess ) ;
                            int timeIncrement = GribNumbers.int4(raf);
                            //System.out.println( "PDS timeIncrement=" + timeIncrement ) ;

                            // 54 - 58
                            int indicatorTR = raf.ReadByte();
                            //System.out.println( "PDS indicatorTR=" + indicatorTR ) ;

                            int lengthTR = GribNumbers.int4(raf);
                            //System.out.println( "PDS lengthTR=" + lengthTR ) ;

                            //int indicatorSF = raf.read();
                            //System.out.println( "PDS indicatorSF=" + indicatorSF ) ;

                            //int incrementSF = GribNumbers.int4( raf );
                            //System.out.println( "PDS incrementSF=" + incrementSF ) ;
                        }
                        break;
                    } // cases 0-8

                // Radar product

                case 20:
                    {

                        parameterCategoryId = raf.ReadByte();
                        //System.out.println( "PDS parameterCategory=" + 
                        //parameterCategory );

                        parameterNumber = raf.ReadByte();
                        //System.out.println( "PDS parameterNumber=" + parameterNumber );

                        typeGenProcess = raf.ReadByte();
                        //System.out.println( "PDS typeGenProcess=" + typeGenProcess );

                        backGenProcess = raf.ReadByte();
                        //System.out.println( "PDS backGenProcess=" + backGenProcess );

                        hoursAfter = GribNumbers.int2(raf);
                        //System.out.println( "PDS hoursAfter=" + hoursAfter );

                        minutesAfter = raf.ReadByte();
                        //System.out.println( "PDS minutesAfter=" + minutesAfter );

                        timeRangeUnitCode = raf.ReadByte();
                        //System.out.println( "PDS timeRangeUnitCode=" + timeRangeUnitCode );

                        forecastTime = GribNumbers.int4(raf);
                        //System.out.println( "PDS forecastTime=" + forecastTime );

                        break;
                    } // case 20

                // Satellite Product

                case 30:
                    {

                        parameterCategoryId = raf.ReadByte();
                        //System.out.println( "PDS parameterCategory=" + parameterCategory );

                        parameterNumber = raf.ReadByte();
                        //System.out.println( "PDS parameterNumber=" + parameterNumber );

                        typeGenProcess = raf.ReadByte();
                        //System.out.println( "PDS typeGenProcess=" + typeGenProcess );

                        backGenProcess = raf.ReadByte();
                        //System.out.println( "PDS backGenProcess=" + backGenProcess );

                        nb = raf.ReadByte();
                        //System.out.println( "PDS numberOfBits =" + numberOfBits );
                        for (int j = 0; j < nb; j++)
                            SupportClass.Skip(raf, 10);
                        break;
                    } // case 30

                // CCITTIA5 character string

                case 254:
                    {

                        parameterCategoryId = raf.ReadByte();
                        //System.out.println( "PDS parameterCategory=" + 
                        //parameterCategory );

                        parameterNumber = raf.ReadByte();
                        //System.out.println( "PDS parameterNumber=" + parameterNumber );

                        //numberOfChars = GribNumbers.int4( raf );
                        //System.out.println( "PDS numberOfChars=" + 
                        //numberOfChars );
                        break;
                    } // case 254


                default:
                    break;

            } // end switch



        }

        // --Commented out by Inspection START (11/21/05 2:24 PM):
        //   /**
        //    * Get the byte length of this section.
        //    *
        //    * @return length in bytes of this section
        //    */
        //   public final int getLength()
        //   {
        //      return length;
        //   }
        // --Commented out by Inspection STOP (11/21/05 2:24 PM)

        // --Commented out by Inspection START (11/21/05 2:24 PM):
        //   /**
        //    * Number of this section, should be 4
        //    */
        //   public final int getSection()
        //   {
        //      return section;
        //   }
        // --Commented out by Inspection STOP (11/21/05 2:24 PM)

        /// <summary> product Definition  Name.</summary>
        /// <returns> ProductDefinitionName
        /// </returns>
        public System.String getProductDefinitionName()
        {
            return getProductDefinitionName(productDefinition);
        }

        /// <summary> productDefinition  Name.
        /// from code table 4.0.
        /// </summary>
        /// <param name="productDefinition">
        /// </param>
        /// <returns> ProductDefinitionName
        /// </returns>
        static public System.String getProductDefinitionName(int productDefinition)
        {
            switch (productDefinition)
            {


                case 0: return "Analysis/forecast at horizontal level/layer";

                case 1: return "Individual ensemble forecast, control and perturbed";

                case 2: return "Derived forecast on all ensemble members";

                case 3: return "Derived forecasts on cluster of ensemble members over rectangular area";

                case 4: return "Derived forecasts on cluster of ensemble members over circular area";

                case 5: return "Probability forecasts at a horizontal level";

                case 6: return "Percentile forecasts at a horizontal level";

                case 7: return "Analysis or forecast error at a horizontal level";

                case 8: return "Average, accumulation, extreme values or other statistically processed value at a horizontal level";

                case 20: return "Radar product";

                case 30: return "Satellite product";

                case 254: return "CCITTIA5 character string";


                default: return "Unknown";

            }
        }



    }
}