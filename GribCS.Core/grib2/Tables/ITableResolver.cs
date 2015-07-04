using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGribCS.GribCS.grib2.Tables
{
    public interface ITableResolver
    {
       ParamCategory ResolveParameterCategory(int pMasterTableVersion, int pLocalTableVersion, int pCategory);

       ParameterDefinition ResolveParameter(int pMasterTableVersion, int pLocalTableVersion, int pCategory, int pParamNumber);
    }
}
