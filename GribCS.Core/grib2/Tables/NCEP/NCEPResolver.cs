using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGribCS.GribCS.grib2.Tables.NCEP
{
    public class NCEPResolver : ITableResolver
    {


        public ParamCategory ResolveParameterCategory(int pMasterTableVersion, int pLocalTableVersion, int pCategory)
        {
            throw new NotImplementedException();
        }


        public ParameterDefinition ResolveParameter(int pMasterTableVersion, int pLocalTableVersion, int pCategory, int pParamNumber)
        {
            throw new NotImplementedException();
        }
    }
}
