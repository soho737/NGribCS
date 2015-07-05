using NGribCS.grib2.Tables;
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;




namespace NGribCS.GribCS.grib2.Tables.WMO
{
    public class WMOResolver : ITableResolver
    {
        private static XElement _xDoc;

        public ParamCategory ResolveParameterCategory(int pDisciplineId, int pMasterTableVersion, int pLocalTableVersion, int pCategory)
        {

            LoadXML();

            XElement catElement = getCategoryElement(pDisciplineId, 14, pCategory);
            if (catElement == null)
                return new ParamCategory("UNDEFINED", pCategory);
            else
            {
                ParamCategory dbg = new ParamCategory(catElement.Element(nsXName("CategoryName")).Value, pCategory);
                return new ParamCategory(catElement.Element(nsXName("CategoryName")).Value, pCategory);

            }
        }

        public Discipline ResolveDiscipline(int pDisciplineId)
        {
            LoadXML();
            XElement discElement = getDisciplineElement(pDisciplineId);


            if (discElement == null)
                return new Discipline(pDisciplineId, "UNDEFINED");
            else
                return new Discipline(pDisciplineId, discElement.Element(nsXName("DisciplineName")).Value);
        }

        public ParameterDefinition ResolveParameter(int pDiscipline, int pMasterTableVersion, int pLocalTableVersion, int pCategory, int pParamNumber)
        {
            LoadXML();

            if (pCategory==0)
            {
                int x = 01;
            }

            XElement parmElement = getParameterElement(pDiscipline, pMasterTableVersion, pCategory, pParamNumber);

            if (parmElement == null)
                return new ParameterDefinition(pParamNumber, "UNDEFINED", "UDEF", "XXX");

            return new ParameterDefinition(pParamNumber, parmElement.Element(nsXName("Name")).Value, parmElement.Element(nsXName("Abbreviation")).Value, parmElement.Element(nsXName("Unit")).Value);
        }




        private XElement getDisciplineElement(int pDisciplineId)
        {

            XElement pDisciplines = _xDoc.Element(nsXName("Disciplines"));
            if (pDisciplines.Elements(nsXName("Discipline")).Count(x => (int)x.Element(nsXName("DisciplineId")) == pDisciplineId) == 1)   
                return pDisciplines.Elements(nsXName("Discipline")).Single(x => (int)x.Element(nsXName("DisciplineId")) == pDisciplineId);
            else
                return null;

  

        }

        private XElement getTableElement(int pDisciplineId, int pVersion)
        {
            XElement xDiscipline = getDisciplineElement(pDisciplineId);
            if (xDiscipline == null)
                return null;
            XElement pTables = xDiscipline.Element(nsXName("ParameterTables"));
            if (pTables == null)
                return null;

            XElement tableElement = pTables.Elements(nsXName("ParameterTable")).Single(x=> (int)x.Element(nsXName("Version")) == 14);

            return tableElement;
        }

        private XElement getCategoryElement(int pDisciplineId, int pVersion, int pCategory)
        {

            // Add an Exception for ambigious data
            XElement tableElement = getTableElement(pDisciplineId, pVersion);
            if (tableElement == null)
                return null;
            
            XElement catsElement = tableElement.Element(nsXName("ParameterCategories"));
            if (catsElement == null)
                return null;

            if (catsElement.Elements(nsXName("ParameterCategory")).Count(x => (int)x.Element(nsXName("CategoryId")) == pCategory) == 1)
                return catsElement.Elements(nsXName("ParameterCategory")).Single(x => (int)x.Element(nsXName("CategoryId")) == pCategory);
            else
                return null;
         
        }

        private XElement getParameterElement(int pDiscipline, int pVersion, int pCategory, int pNumber)
        {

            XElement catElement = getCategoryElement(pDiscipline, pVersion, pCategory);
            if (catElement == null)
                return null;

           

            XElement parmsElement = catElement.Element(nsXName("Parameters"));

            if (parmsElement.Elements(nsXName("Parameter")).Count(x => (int) x.Element(nsXName("Number")) == pNumber) == 1)
                return parmsElement.Elements(nsXName("Parameter")).Single(x => (int) x.Element(nsXName("Number")) == pNumber);
            else
                return null;

        }

        private XName nsXName(string pLocalName)
        {
            return XName.Get(pLocalName, "http://tempuri.org/Grib2MasterTable.xsd");
        }

        public void LoadXML()
        {

            if (_xDoc == null)
            {
                Assembly a = this.GetType().Assembly;
                Stream s = a.GetManifestResourceStream("NGribCS.grib2.Tables.WMO.Grib2MasterTable.xml");

                _xDoc = XElement.Load(s);
                _xDoc.Save(@"E:\text.xml");
            }
        }





       
    }
}
