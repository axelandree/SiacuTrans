﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper.ResponseGrid;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper
{
    public class PackagesListHelper
    {
        public List<PackageLHelper> package { get; set; }

        // LISTAS PARA LAS GRID
        public List<HELPER_ITEM.ListPackageClient> ListPackageClient { get; set; }

    }
}