using Cibb.Core.Utility;
using System.Web.Mvc;

namespace Cibb.Web.UI2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //if (Request.Browser.IsMobileDevice)
            //    return RedirectToAction("Index", "News");
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


        public ActionResult ApplicationForm()
        {
            return File(FileHelper.GetFile("Application","docx"), "application/octet-stream", "入会申请表.docx");
        }
    }
}