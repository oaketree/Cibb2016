using Gygl.Web.BLL.Contribute;
using Gygl.Web.BLL.Contribute.Models;
using Gygl.Web.BLL.Share;
using System.Web.Mvc;

namespace Gygl.Web.UI.Controllers
{
    public class ContributeController : Controller
    {
        private IWebService webservice;
        public ContributeController(IWebService webservice)
        {
            this.webservice = webservice;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContributeList()
        {
            if (!webservice.logincheck())
                return RedirectToAction("Prompt", "Register", new Jump { aname = "ContributeList", cname = "Contribute" });
            return View();
        }

        public ActionResult AddContribute()
        {
            if (!webservice.logincheck())
                return RedirectToAction("Prompt", "Register", new Jump { aname = "AddContribute", cname = "Contribute" });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddContribute(ContributeViewModel cvm)
        {
            if (webservice.addContribute(cvm,ModelState))
                return View("ContributeList");
            return View();
        }

        //public ActionResult AddAccessory(int a)
        //{
        //    if (!webservice.logincheck())
        //        return RedirectToAction("Prompt", "Register", new Jump { aname = "AddContribute", cname = "Contribute" });
        //    return View(a);
        //}

    }
}