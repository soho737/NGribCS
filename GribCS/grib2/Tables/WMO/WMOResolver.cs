using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGribCS.GribCS.grib2.Tables.Generic
{
    public class GenericResolver : ITableResolver
    {
        public ParamCategory ResolveParameterCategory(int pMasterTableVersion, int pLocalTableVersion, int pCategory)
        {
            // Here we use the latest master table v14;
            switch (pCategory)
            {
                case 0:
                    return new ParamCategory("Temperature", 0);
                case 1:
                    return new ParamCategory("Moisture", 0);
                case 2:
                    return new ParamCategory("Momentum", 0);
                case 3:
                    return new ParamCategory("Mass", 0);
                default:
                    return new ParamCategory("Unknown", pCategory);




            }

        }
    }
}
