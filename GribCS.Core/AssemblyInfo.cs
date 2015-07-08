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

using System.Reflection;
using System.Runtime.CompilerServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

// TODO: Review the values of the assembly attributes

[assembly: AssemblyTitle("NGribCS")]
[assembly: AssemblyDescription("Modernized C# GRIB decoder")]
[assembly: AssemblyCompany("Karsten Kaehler")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]


// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Revision
//      Build Number
//
// You can specify all the values or you can default the Revision and Build Numbers
// by using the '*' as shown below:

[assembly: AssemblyVersion("1.0.*")]

//
// In order to sign your assembly you must specify a key to use. Refer to the 
// Microsoft .NET Framework documentation for more information on assembly signing.
//
// Use the attributes below to control which key is used for signing. 
//
// Notes: 
//   (*) If no key is specified, the assembly is not signed.
//   (*) KeyName refers to a key that has been installed in the Crypto Service
//       Provider (CSP) on your machine. KeyFile refers to a file which contains
//       a key.
//   (*) If the KeyFile and the KeyName values are both specified, the 
//       following processing occurs:
//       (1) If the KeyName can be found in the CSP, that key is used.
//       (2) If the KeyName does not exist and the KeyFile does exist, the key 
//           in the KeyFile is installed into the CSP and used.
//   (*) In order to create a KeyFile, you can use the sn.exe (Strong GridTemplateName) utility.
//       When specifying the KeyFile, the location of the KeyFile should be
//       relative to the project output directory which is
//       %Project Directory%\obj\<configuration>. For example, if your KeyFile is
//       located in the project directory, you would specify the AssemblyKeyFile 
//       attribute as [assembly: AssemblyKeyFile("..\..\mykey.snk")]
//   (*) Delay Signing is an advanced option - see the Microsoft .NET Framework
//       documentation for more information on this.
//

[assembly: AssemblyConfiguration("")]
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]


