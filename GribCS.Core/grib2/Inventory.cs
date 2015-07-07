using NGribCS.Grib2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGribCS.grib2
{
    public class Inventory
    {
        private List<InventoryItem> _inventoryItems;

        public List<InventoryItem> InventoryItems
        {
            get
            {
               return _inventoryItems;
            }
        }

        public Inventory (List<InventoryItem> pItems)
        {
            _inventoryItems = pItems;
        }


        public List<Grib2ProductId> GetDisctinctProducts()
        {
            return _inventoryItems.Select(x => x.Product.ProductIdentification).Distinct().ToList();
        }


        public List<InventoryItem> GetAllRecordsForProduct(int pDiscipline, int pCategory, int pParameter)
        {
            return _inventoryItems.Where(x => x.Product.ProductIdentification.Discipline.DisciplineId == pDiscipline && x.Product.ProductIdentification.Category.Id == pCategory && x.Product.ProductIdentification.Parameter.Id == pParameter).ToList();
        }

        public IEnumerable<DateTime> GetAllValidTimesForProduct(int pDiscipline, int pCategory, int pParameter)
        {
            List<InventoryItem> ivs = GetAllRecordsForProduct(pDiscipline, pCategory, pParameter);
            return ivs.Select(x => x.Product.ValidTime).Distinct();
        }

        public IEnumerable<Grib2SurfaceDefinition> GetAllSurfacesForProduct(int pDiscipline, int pCategory, int pParameter)
        {
            List<InventoryItem> ivs = GetAllRecordsForProduct(pDiscipline, pCategory, pParameter);
            return ivs.Select(x => x.Product.PDS.SurfaceDefinition).Distinct();
        }

    }
}
