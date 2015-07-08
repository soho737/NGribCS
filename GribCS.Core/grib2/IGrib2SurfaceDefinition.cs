using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGribCS.Grib2
{
    public interface IGrib2SurfaceDefinition
    {
        int TypeFirstFixedSurface { get; }
        string TypeFirstFixedSurfaceName { get; }

        int TypeSecondFixedSurface { get; }
        string TypeSecondFixedSurfaceName { get; }
        float ValueFirstFixedSurface { get; }
        float ValueSecondFixedSurface { get; }

        string UnitFirstFixedSurface { get; }
        string UnitSecondFixedSurface { get; }
    }
}
