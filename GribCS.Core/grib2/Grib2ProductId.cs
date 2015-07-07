using NGribCS.grib2.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGribCS.grib2
{
    public class Grib2ProductId : System.IEquatable<Grib2ProductId>
    {
        private Discipline _discipline;
        private ParamCategory _category;
        private ParameterDefinition _param;


        public Discipline Discipline
        {
            get
            {
                return _discipline;
            }
        }

     

        public ParamCategory Category
        {
            get
            {
                return _category;
            }
        }

        public ParameterDefinition Parameter
        {
            get
            {
                return _param;
            }
        }

      
        public Grib2ProductId(Discipline pDiscipline, ParamCategory pCategory, ParameterDefinition pParam)
        {
            _discipline = pDiscipline;
            _category = pCategory;
            _param = pParam;
        }


        public override int GetHashCode()
        {
            return _discipline.DisciplineId * 1000 + _category.Id * 100 + _param.Id;
        }


        public bool Equals(Grib2ProductId other)
        {
            if (!other.GetType().Equals(typeof(Grib2ProductId)))
                return false;

            if (!this.Discipline.DisciplineId.Equals(other.Discipline.DisciplineId))
                return false;

            if (!this.Category.Id.Equals(other.Category.Id))
                return false;

            if (!this.Parameter.Id.Equals(other.Parameter.Id))
                return false;

            return true;
        }
    }
}
