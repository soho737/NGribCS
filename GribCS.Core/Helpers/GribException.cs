using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGribCS.Helpers
{

    /// <summary>
    /// This is the base class for all exceptions to be thrown on purpose by NGribCS
    /// opposed to Exceptions thrown due to bugs
    /// </summary>
    public class GribException : Exception
    {
        public GribException(System.String msg):base(msg)
		{
		}
    }
}
