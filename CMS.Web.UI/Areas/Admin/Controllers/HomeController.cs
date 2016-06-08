using Cibb.Core.Cache;
using CMS.BLL.Admin.Models;
using CMS.BLL.Attributes;
using System.Web.Mvc;
using System.Web.Services;

namespace CMS.Web.UI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        [Permission]
        public ActionResult Index()
        {
            var login = (LoginAdminInfo)SessionHelper.Get("LoginAdminInfo");
            ViewBag.User =login.DisplayName;
            return View();
        }
        public PartialViewResult Menu() {
            return PartialView();
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