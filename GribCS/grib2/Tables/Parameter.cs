using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGribCS.GribCS.grib2.Tables
{
    public class Parameter
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public string Abbreviation { get; protected set; }

        public string Unit { get; protected set; }


        public Parameter (int pId, string pName, string pAbbr, string pUnit)
        {
            Id = pId;
            Name = pName;
            Abbreviation = pAbbr;
            Unit = pUnit;
        }
    }
}
