using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGribCS.GribCS.grib2.Tables
{
    public class ParamCategory
    {
        public string Name { get; protected set; }
        public int Id { get; protected set; }

        public ParamCategory(string pName, int pId)
        {
            Name = pName;
            Id = pId;
        }
    }
}
