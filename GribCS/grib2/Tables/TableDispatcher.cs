using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGribCS.GribCS.grib2.Tables
{
    public class TableDispatcher
    {
        public static ITableResolver GetResolver(int centerId)
        {
            if (centerId == -7)
            {
                return new NCEP.NCEPResolver();
            }
            else
            {
                return new Generic.GenericResolver();
            }

        }
    
    }
}
