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

using NGribCS;
using NGribCS.Helpers;
using System;


namespace NGribCS.Grib2
{
	
	/// <summary> Class which represents a Category from a parameter table.
	/// A parameter consists of a discipline( ie Meteorological_products), 
	/// a Category( ie Temperature ) and a number that refers to a name( ie Temperature)
	/// </summary>
	

	public class Category
	{
        
        /// <summary> returns the number of this Category.</summary>
		/// <returns> int
		/// </returns>
		/// <summary> number value of this Category.</summary>
		/// <param name="number">of Category
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
		

		
		/// <summary> each category has a unique number.</summary>
		private int number;
		
		

		/// <summary> parameter - a HashMap of Parameters.</summary>
		private System.Collections.Hashtable parameter;
		
		/// <summary>  Constructor for a Category.</summary>
		public Category()
		{
			number = - 1;
			parameter = new System.Collections.Hashtable();
		}
		
		
		/// <summary> given a Parameter number returns Parameter object for this Category.</summary>
		/// <param name="paramNumber">
		/// </param>
		/// <returns> Parameter
		/// </returns>
		public Parameter getParameter(int paramNumber)
		{
			if (parameter.ContainsKey(System.Convert.ToString(paramNumber)))
			{
				return (Parameter) parameter[System.Convert.ToString(paramNumber)];
			}
			else
			{
				return new Parameter(paramNumber, "Unknown", "Unknown", "Unknown");
			}
		}
		
		
		/// <summary> add this Parameter to this Category.</summary>
		/// <param name="param">object
		/// </param>
		public void  setParameter(Parameter param)
		{
			parameter[System.Convert.ToString(param.Number)] = param;
		}
	} // end Category
}