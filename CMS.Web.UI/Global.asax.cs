﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CMS.Web.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DependencyResolver.SetResolver(new UnityDependencyResolver());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
