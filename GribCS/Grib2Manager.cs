using NGribCS.Grib2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NGribCS
{
    public class Grib2Manager : IDisposable
    {

        private System.IO.Stream _sourceStream;
        private Grib2Input _g2i;


        /// <summary>
        /// Initializes a new instance of the Grib2Manager from a grib file
        /// </summary>
        /// <param name="pFileName">The grib2-File that should be opened</param>
        /// <param name="pInMemoryProcessing">Process the grib2-File in memory. This is significantly faster, but consumes more ressources</param>
        public Grib2Manager(string pFileName, bool pInMemoryProcessing)
        {
            if (pInMemoryProcessing)
            {
                using (FileStream fs = new System.IO.FileStream(pFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    _sourceStream = new MemoryStream();
                    copyStream(fs, _sourceStream);
                }
            }
            else
            {
                _sourceStream = new System.IO.FileStream(pFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            }

            _g2i = new Grib2Input(_sourceStream);
            _g2i.scan(false, false);
        }


        public void Dispose()
        {
            if (_sourceStream != null)
                _sourceStream.Dispose();
        }

        private static void copyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }



    }
}
