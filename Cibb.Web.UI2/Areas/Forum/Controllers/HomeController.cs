using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace Cibb.Web.UI2.Areas.Forum.Controllers
{
    public class HomeController : Controller
    {
        // GET: Forum/Home
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Bottom()
        {
            return PartialView();
        }

        public PartialViewResult Top()
        {
            return PartialView();
        }
        
        public ActionResult Media()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Keynote()
        {
            return View();
        }

        public ActionResult Special()
        {
            return View();
        }

        public ActionResult Organizor()
        {
            return View();
        }

        /// <summary>
        /// session不自动关闭
        /// </summary>
        [WebMethod(EnableSession = true)]
        public void PokePage()
        {

        }

    }
}