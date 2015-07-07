using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGribCS.grib2
{
    public class Grib2SurfaceDefinition : IGrib2SurfaceDefinition
    {



        public int TypeFirstFixedSurface { get; protected set; }
  
        public int TypeSecondFixedSurface {get; protected set;}

        public float ValueFirstFixedSurface {get; protected set;}

        public float ValueSecondFixedSurface { get; protected set; }


        public string TypeFirstFixedSurfaceName
        {
            get
            {
                return getTypeSurfaceName(TypeFirstFixedSurface);
            }
        }

        public string UnitFirstFixedSurface
        {
            get
            {
                return getTypeSurfaceUnit(TypeFirstFixedSurface);
            }
        }


        public string UnitSecondFixedSurface
        {
            get
            {
                return getTypeSurfaceUnit(TypeSecondFixedSurface);
            }
        }
          
        public string TypeSecondFixedSurfaceName
        {
            get
            {
              return getTypeSurfaceName(TypeSecondFixedSurface);
            }
        }

     
    



        /// <summary> type of vertical coordinate: Name
        /// code table 4.5.
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns> SurfaceName
        /// </returns>
        static public System.String getTypeSurfaceName(int id)
        {
            switch (id)
            {

                case 0: return "Reserved";

                case 1: return "Ground or water surface";

                case 2: return "Cloud base level";

                case 3: return "Level of cloud tops";

                case 4: return "Level of 0o C isotherm";

                case 5: return "Level of adiabatic condensation lifted from the surface";

                case 6: return "Maximum wind level";

                case 7: return "Tropopause";

                case 8: return "Nominal top of the atmosphere";

                case 9: return "Sea bottom";

                case 20: return "Isothermal level";

                case 100: return "Isobaric surface";

                case 101: return "Mean sea level";

                case 102: return "Specific altitude above mean sea level";

                case 103: return "Specified height level above ground";

                case 104: return "Sigma level";

                case 105: return "Hybrid level";

                case 106: return "Depth below land surface";

                case 107: return "Isentropic 'theta' level";

                case 108: return "Level at specified pressure difference from ground to level";

                case 109: return "Potential vorticity surface";

                case 111: return "Eta* level";

                case 117: return "Mixed layer depth";

                case 160: return "Depth below sea level";

                case 200: return "entire atmosphere layer";

                case 201: return "entire ocean layer";

                case 204: return "Highest tropospheric freezing level";

                case 206: return "Grid scale cloud bottom level";

                case 207: return "Grid scale cloud top level";

                case 209: return "Boundary layer cloud bottom level";

                case 210: return "Boundary layer cloud top level";

                case 211: return "Boundary layer cloud layer";

                case 212: return "Low cloud bottom level";

                case 213: return "Low cloud top level";

                case 214: return "Low cloud layer";

                case 222: return "Middle cloud bottom level";

                case 223: return "Middle cloud top level";

                case 224: return "Middle cloud layer";

                case 232: return "High cloud bottom level";

                case 233: return "High cloud top level";

                case 234: return "High cloud layer";

                case 235: return "Ocean isotherm level";

                case 236: return "Layer between two depths below ocean surface";

                case 237: return "Bottom of ocean mixed layer";

                case 238: return "Bottom of ocean isothermal layer";

                case 242: return "Convective cloud bottom level";

                case 243: return "Convective cloud top level";

                case 244: return "Convective cloud layer";

                case 245: return "Lowest level of the wet bulb zero";

                case 246: return "Maximum equivalent potential temperature level";

                case 247: return "Equilibrium level";

                case 248: return "Shallow convective cloud bottom level";

                case 249: return "Shallow convective cloud top level";

                case 251: return "Deep convective cloud bottom level";

                case 252: return "Deep convective cloud top level";

                case 255: return "Missing";

                default: return "Unknown=" + id;

            }
        } // end getTypeSurfaceName

        /// <summary> type of vertical coordinate: short Name
        /// derived from code table 4.5.
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns> SurfaceNameShort
        /// </returns>
        static public System.String getTypeSurfaceNameShort(int id)
        {
            switch (id)
            {

                case 1: return "surface";

                case 2: return "cloud_base";

                case 3: return "cloud_tops";

                case 4: return "zeroDegC_isotherm";

                case 5: return "adiabatic_condensation_lifted";

                case 6: return "maximum_wind";

                case 7: return "tropopause";

                case 8: return "atmosphere_top";

                case 9: return "sea_bottom";

                case 20: return "isotherm";

                case 100: return "pressure";

                case 101: return "msl";

                case 102: return "altitude_above_msl";

                case 103: return "height_above_ground";

                case 104: return "sigma";

                case 105: return "hybrid";

                case 106: return "depth_below_surface";

                case 107: return "isentrope";

                case 108: return "pressure_difference";

                case 109: return "potential_vorticity_surface";

                case 111: return "eta";

                case 117: return "mixed_layer_depth";

                case 160: return "depth_below_sea";

                case 200: return "entire_atmosphere";

                case 201: return "entire_ocean";

                case 204: return "highest_tropospheric_freezing";

                case 206: return "grid_scale_cloud_bottom";

                case 207: return "grid_scale_cloud_top";

                case 209: return "boundary_layer_cloud_bottom";

                case 210: return "boundary_layer_cloud_top";

                case 211: return "boundary_layer_cloud";

                case 212: return "low_cloud_bottom";

                case 213: return "low_cloud_top";

                case 214: return "low_cloud";

                case 222: return "middle_cloud_bottom";

                case 223: return "middle_cloud_top";

                case 224: return "middle_cloud";

                case 232: return "high_cloud_bottom";

                case 233: return "high_cloud_top";

                case 234: return "high_cloud";

                case 235: return "ocean_Isotherm";

                case 236: return "layer_between_two_depths_below_ocean";

                case 237: return "bottom_of_ocean_mixed";

                case 238: return "bottom_of_ocean_isothermal";

                case 242: return "convective_cloud_bottom";

                case 243: return "convective_cloud_top";

                case 244: return "convective_cloud";

                case 245: return "lowest_level_of_the_wet_bulb_zero";

                case 246: return "maximum_equivalent_potential_temperature";

                case 247: return "equilibrium";

                case 248: return "shallow_convective_cloud_bottom";

                case 249: return "shallow_convective_cloud_top";

                case 251: return "deep_convective_cloud_bottom";

                case 252: return "deep convective_cloud_top";

                case 255: return "";


                default: return "Unknown" + id;

            }
        } // end getTypeSurfaceNameShort

        /// <summary> type of vertical coordinate: Units.
        /// code table 4.5.
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns> surfaceUnit
        /// </returns>
        static public System.String getTypeSurfaceUnit(int id)
        {
            switch (id)
            {

                case 20: return "K";

                case 100: return "Pa";

                case 102: return "m";

                case 103: return "m";

                case 106: return "m";

                case 107: return "K";

                case 108: return "Pa";

                case 109: return "K m2 kg-1 s-1";

                case 117: return "m";

                case 160: return "m";

                case 235: return "C 0.1";

                case 237: return "m";

                case 238: return "m";


                default: return "";

            }
        } // end getTypeSurfaceUnit

    
    } // end Grib2ProductDefinitionSection




}

