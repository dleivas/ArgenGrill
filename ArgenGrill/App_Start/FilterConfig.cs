﻿using System.Web.Mvc;

namespace ArgenGrill
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //GlobalFilters.Filters.Add(new RequireHttpsAttribute());
        }
    }
}