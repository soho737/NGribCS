﻿using NGribCS.grib2;
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
    public interface IGrib2ProductDefinitionSection
    {
        int AnalysisGenProcess { get; }
        int BackGenProcess { get; }
        int Coordinates { get; }
        int ForecastTime { get; }
        int TypeGenProcess { get; }
        string getProductDefinitionName();
        TimeRangeUnits TimeRangeUnit { get; }
        int HoursAfter { get; }
        int MinutesAfter { get; }
        int ParameterCategory { get; }
        int ParameterNumber { get; }
        int ProductDefinition { get; }
        int TimeRangeUnitCode { get; }

        Grib2SurfaceDefinition SurfaceDefinition { get; }
  
    }
}
