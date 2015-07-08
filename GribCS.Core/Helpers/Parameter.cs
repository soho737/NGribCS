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

/// <summary> Parameter.java</summary>
/// <author>  Robert Kambic 10/10/03
/// </author>
using System;
using System.Runtime.InteropServices;

namespace NGribCS.Helpers
{
	
	/// <summary> Class which represents a parameter from a PDS parameter table.
	/// A parameter consists of a discipline( ie Meteorological_products), 
	/// a Category( ie Temperature ) and a number that refers to a gridTemplateName( ie Temperature),
	/// Description( ie Temperature at 2 meters), and Units( ie K ).
	/// see <a href="../../Parameters.txt">Parameters.txt</a>
	/// </summary>
    [ComVisible(false)]
	public sealed class Parameter
	{
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> number of parameter.</summary>
		/// <returns> number
		/// </returns>
		/// <summary> sets number of parameter.</summary>
		/// <param gridTemplateName="number">of parameter
		/// </param>
		public int Number
		{
			get
			{
				return number;
			}
			
			set
			{
				this.number = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> gridTemplateName of parameter.</summary>
		/// <returns> gridTemplateName
		/// </returns>
		/// <summary> sets gridTemplateName of parameter.</summary>
		/// <param gridTemplateName="gridTemplateName"> of parameter
		/// </param>
		public System.String Name
		{
			get
			{
				return name;
			}
			
			set
			{
				this.name = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> description of parameter.
		/// 
		/// </summary>
		/// <returns> description
		/// </returns>
		/// <summary> sets description of parameter.</summary>
		/// <param gridTemplateName="description">of parameter
		/// </param>
		public System.String Description
		{
			get
			{
				return description;
			}
			
			set
			{
				this.description = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> unit of parameter.</summary>
		/// <returns> unit
		/// </returns>
		/// <summary> sets unit of parameter.</summary>
		/// <param gridTemplateName="unit">of parameter
		/// </param>
		public System.String Unit
		{
			get
			{
				return unit;
			}
			
			set
			{
				this.unit = value;
			}
			
		}
		/// <summary> parameter number.</summary>
		private int number;
		/// <summary> gridTemplateName of parameter.</summary>
		private System.String name;
		/// <summary> description of parameter.</summary>
		private System.String description;
		/// <summary> unit of Parameter.</summary>
		private System.String unit;
		
		/// <summary> constructor.</summary>
		public Parameter()
		{
			number = - 1;
			name = "undefined";
			description = "undefined";
			unit = "undefined";
		}
		
		/// <summary> constructor.</summary>
		/// <param gridTemplateName="number">
		/// </param>
		/// <param gridTemplateName="gridTemplateName">
		/// </param>
		/// <param gridTemplateName="description">
		/// </param>
		/// <param gridTemplateName="unit">of parameter
		/// </param>
		public Parameter(int number, System.String name, System.String description, System.String unit)
		{
			this.number = number;
			this.name = name;
			this.description = description;
			this.unit = unit;
		}
	}
}