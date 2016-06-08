using System.Web.Mvc;
using System.Web.Routing;

namespace Cibb.Web.UI2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("lmm/{*pathInfo}");
            routes.IgnoreRoute("Views/Forum/{*pathInfo}");
            //routes.MapRoute(
            //    name: null,
            //    url: "news/{id}.html",
            //    defaults: new { controller = "Home", action = "Detail", id = UrlParameter.Optional }
            //);
            routes.MapRoute(null, "NewsDetail/{id}", new { Controller = "News", Action = "Detail" }, new { id = @"\d+" });
            routes.MapRoute(null, "News", new { Controller = "News", Action = "Index", page = 1 });
            routes.MapRoute(null, "News/{page}", new { Controller = "News", Action = "Index" }, new { page = @"\d+" });
            routes.MapRoute(null, "NewsList/{category}", new { Controller = "News", Action = "List", page = 1 }, new { category = @"\d+" });
            routes.MapRoute(null, "NewsList/{category}/{page}", new { Controller = "News", Action = "List" }, new { category = @"\d+", page = @"\d+" });
            routes.MapRoute(null, "BranchDirector", new { Controller = "Council", Action = "BranchDirector", page = 1 });
            routes.MapRoute(null, "BranchDirector/{page}", new { Controller = "Council", Action = "BranchDirector" }, new { page = @"\d+" });
            routes.MapRoute(null, "BranchMember", new { Controller = "Council", Action = "BranchMember", page = 1 });
            routes.MapRoute(null, "BranchMember/{page}", new { Controller = "Council", Action = "BranchMember" }, new { page = @"\d+" });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Cibb.Web.UI2.Controllers" }
            );
        }
    }
}
