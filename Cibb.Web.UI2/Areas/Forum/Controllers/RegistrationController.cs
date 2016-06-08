using Cibb.Web.BLL.Registration;
using Cibb.Web.BLL.Registration.Models;
using System.Web.Mvc;

namespace Cibb.Web.UI2.Areas.Forum.Controllers
{
    public class RegistrationController : Controller
    {
        private IWebService webservice;
        public RegistrationController(IWebService webservice)
        {
            this.webservice = webservice;
        }

        public ActionResult JsonLogin(string u,string p)
        {
            var a=this.webservice.login(u,p);
            return Json(a);
        }

        public void JsonLoginOut()
        {
            this.webservice.loginout();
        }

        public ActionResult JsonLoginCheck()
        {
            var a = this.webservice.logincheck();
            return Json(a);
        }
        public JsonResult CkUserName(string username)
        {
            return Json(this.webservice.ckUserName(username.Trim()), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserModel um)
        {
            if (ModelState.IsValid)
            {
                var a = this.webservice.register(um);
                if (a=="1")
                    return View("SendEmail", um);
                else
                    return View("ErrorEmail",(object)a);
            }
            return View();
        }

        public ActionResult SendEmail(UserModel um)
        {
            return View(um);
        }

        public ActionResult ErrorEmail(string a)
        {
            return View(a);
        }


        //public ActionResult ReSendEmail(int uid)
        //{
        //    return View(uid.ToString());
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ReSendEmail(string uid)
        //{
        //    if (this.webservice.sendEmail(int.Parse(uid)))
        //        return View()
        //    return View(um);
        //}

        public ActionResult Activate(int? uid,string code)
        {
            return View((object)webservice.activate(uid.Value,code));
        }
    }
}