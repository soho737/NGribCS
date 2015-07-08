using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGribCS.Helpers
{
    /// <summary>
    /// This exception is thrown when you try to access a feature currently not supported in NGribCS
    /// </summary>
    public class GribNotSupportedException : GribException
    {
        public GribNotSupportedException(System.String msg)
            : base(msg)
        {
        }
    }
}
