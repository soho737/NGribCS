﻿using NGribCS.Grib2.Tables;
/*
 * This file is part of NGribCS which is a fork of GribCS
 * found at http://sourceforge.net/projects/gribcs/ 
 * 
 * NGribCS is brought to you by Karsten Kaehler <ngribcs@kkaehler.net>
 * 
 * GribCS was made by
 * Seaware AB, PO Box 1244, SE-131 28, Nacka Strand, Sweden, info@seaware.se.
 * 
 * GribCS itself is based on an automatic conversion of JGRIB Beta 7 
 * (http://jgrib.sourceforge.net/) from Java to C#.
 * 
 * Java-code: Copyright 1997-2006 Unidata Program Center/University 
 * Corporation for Atmospheric Research, P.O. Box 3000, Boulder, CO 80307,
 * support@unidata.ucar.edu.
 * 
 * NGribCS is free software: you can redistribute it and/or modify it under 
 * the terms of the GNU Lesser General Public License as published by the 
 * Free Software Foundation, either version 3 of the License, or (at your 
 * option) any later version.
 * 
 * NGribCS is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of 
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License 
 * along with GribCS.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGribCS.GribCS.grib2.Tables
{
    public class Grib2Resolver
    {
        public static Discipline ResolveDiscipline(int pDisciplineId, int pCenterId)
        {
            ITableResolver tr = TableDispatcher.GetResolver(pCenterId);
            return tr.ResolveDiscipline(pDisciplineId);
        }

        public static ParamCategory ResolveParameterCategory(int pDisciplineId, int pCenterId, int pMasterVersion, int pLocalVersion, int pCategoryId)
        {
           ITableResolver tr =  TableDispatcher.GetResolver(pCenterId);
           return tr.ResolveParameterCategory(pDisciplineId, pMasterVersion, pLocalVersion, pCategoryId);
        }

        public static ParameterDefinition ResolveParameter(int pDisciplineId, int pCenterId, int pMasterVersion, int pLocalVersion, int pCategoryId, int pParamNumber)
        {
            ITableResolver tr = TableDispatcher.GetResolver(pCenterId);
            return tr.ResolveParameter(pDisciplineId, pMasterVersion, pLocalVersion, pCategoryId, pParamNumber);
        }
    }
}
