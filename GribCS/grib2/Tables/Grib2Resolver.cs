using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGribCS.GribCS.grib2.Tables
{
    public class Grib2Resolver
    {
        public static ParamCategory ResolveParameterCategory(int pCenterId, int pMasterVersion, int pLocalVersion, int pCategoryId)
        {
           ITableResolver tr =  TableDispatcher.GetResolver(pCenterId);
           return tr.ResolveParameterCategory(pMasterVersion, pLocalVersion, pCategoryId);
        }


    }
}
