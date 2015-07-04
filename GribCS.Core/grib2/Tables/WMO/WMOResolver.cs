
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

        public ParamCategory ResolveParameterCategory(int pMasterTableVersion, int pLocalTableVersion, int pCategory)
        {

            LoadXML();

            XElement catElement = getCategoryElement(14, pCategory);
            if (catElement == null)
                return new ParamCategory("UNDEFINED", pCategory);
            else
            {
                ParamCategory dbg = new ParamCategory(catElement.Element(nsXName("CategoryName")).Value, pCategory);
                return new ParamCategory(catElement.Element(nsXName("CategoryName")).Value, pCategory);

            }
        }

        public ParameterDefinition ResolveParameter(int pMasterTableVersion, int pLocalTableVersion, int pCategory, int pParamNumber)
        {
            LoadXML();

            XElement parmElement = getParameterElement(pMasterTableVersion, pCategory, pParamNumber);

            if (parmElement == null)
                return new ParameterDefinition(pParamNumber, "UNDEFINED", "UDEF", "XXX");

            return new ParameterDefinition(pParamNumber, parmElement.Element(nsXName("Name")).Value, parmElement.Element(nsXName("Abbreviation")).Value, parmElement.Element(nsXName("Number")).Value);
        }

        private XElement getTableElement(int pVersion)
        {
            XElement pTables = _xDoc.Element(nsXName("ParameterTables"));
            XElement tableElement = pTables.Elements(nsXName("ParameterTable")).Single(x=> (int)x.Element(nsXName("Version")) == 14);

            return tableElement;
        }

        private XElement getCategoryElement(int pVersion, int pCategory)
        {

            // Add an Exception for ambigious data
            XElement tableElement = getTableElement(pVersion);
            XElement catsElement = tableElement.Element(nsXName("ParameterCategories"));


            if (catsElement.Elements(nsXName("ParameterCategory")).Count(x => (int)x.Element(nsXName("CategoryId")) == pCategory) == 1)
                return catsElement.Elements(nsXName("ParameterCategory")).Single(x => (int)x.Element(nsXName("CategoryId")) == pCategory);
            else
                return null;
         
        }

        private XElement getParameterElement(int pVersion, int pCategory, int pNumber)
        {
            XElement tableElement = getTableElement(pVersion);
            XElement catsElement = tableElement.Element(nsXName("ParameterCategories"));
            XElement catElement;

            if (catsElement.Elements(nsXName("ParameterCategory")).Count(x => (int)x.Element(nsXName("CategoryId")) == pCategory) == 1)
                catElement = catsElement.Elements(nsXName("ParameterCategory")).Single(x => (int)x.Element(nsXName("CategoryId")) == pCategory);
            else
                return null;

            XElement parmsElement = catElement.Element(nsXName("Parameters"));

            if (parmsElement.Elements(nsXName("Parameter")).Count(x => (int) x.Element(nsXName("Number")) == pNumber) == 1)
                return parmsElement.Elements(nsXName("Parameter")).Single(x => (int) x.Element(nsXName("Number")) == pNumber);
            else
                return null;

        }

        private XName nsXName(string pLocalName)
        {
            return XName.Get(pLocalName, "http://tempuri.org/MasterMeteo.xsd");
        }

        public void LoadXML()
        {

            if (_xDoc == null)
            {
                Assembly a = this.GetType().Assembly;
                Stream s = a.GetManifestResourceStream("NGribCS.grib2.Tables.WMO.MasterTableMeteo.xml");

                _xDoc = XElement.Load(s);
                _xDoc.Save(@"E:\text.xml");
            }
        }




    }
}
