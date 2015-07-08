using NGribCS.Grib2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGribCS.Grib2
{
    public class InventoryItem
    {
        public int SourceIndex { get;  private set; }

        public int ProductIndex { get; private  set; }

        public Grib2Product Product { get; private set; }

        public InventoryItem (int pSourceIdx, int pProdIdx, Grib2Product pProduct)
        {
            SourceIndex = pSourceIdx;
            ProductIndex = pProdIdx;
            Product = pProduct;
        }

    }
}
