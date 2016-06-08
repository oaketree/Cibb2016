using Gygl.Web.BLL.Register;
using Gygl.Web.BLL.Register.Models;
using Gygl.Web.BLL.Share;
using Microsoft.Practices.Unity;
using System.Web.Mvc;

namespace Gygl.Web.UI.Controllers
{
    public class RegisterController : Controller
    {
        [Dependency]
        public IWebService webservice { get; set; }

        //private IWebService webservice;
        //public RegisterController(IWebService webservice)
        //{
        //    this.webservice = webservice;
        //}


        public ActionResult Login()
        {
            return View();
        }

        public ActionResult JsonLogin(string u, string p,bool auto=false)
        {
            var a = this.webservice.login(u, p, auto);
            return Json(a);
        }

        public ActionResult JsonLoginCheck()
        {
            var a = this.webservice.logincheck();
            return Json(a);
        }

        public void JsonLoginOut()
        {
            this.webservice.loginout();
        }

        public ActionResult JsonForget(string u,string e)
        {
            var a = webservice.forget(u, e);
            return Json(a);
        }

        // GET: Register
        public ActionResult Reg()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reg(RegViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                var a=this.webservice.reg(rvm);
                if (a == "1")
                    return View("SendEmail", rvm);
                else
                    return View("ErrorEmail", (object)a);
            }
            return View();
        }

        public ActionResult Manage()
        {
            var user = this.webservice.getUser();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(UserViewModel uvm)
        {
            if (ModelState.IsValid)
            {
                this.webservice.editUser(uvm);
            }
            return View(uvm);
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        public ActionResult SendEmail(RegViewModel uvm)
        {
            return View(uvm);
        }

        public ActionResult ErrorEmail(string a)
        {
            return View(a);
        }

        public ActionResult Activate(int? uid, string code)
        {
            return View((object)webservice.activate(uid.Value, code));
        }

        public ActionResult ChangePassword(int? uid, string code)
        {
            var a = webservice.getUser(uid.Value, code);
            if (a == null)
            {
                return RedirectToAction("ForgetPassword");
            }
            else
            {
                return View(a);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(PasswordViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                return View("ChangePasswordSuccess", this.webservice.updatePass(pvm));
            }
            return View(pvm);
        }

        public ActionResult ChangePasswordSuccess(PasswordViewModel pvm)
        {
            return View(pvm);
        }

        public JsonResult CkUserName(string username)
        {
            return Json(this.webservice.ckUserName(username.Trim()), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Prompt(Jump model)
        {
            return View(model);
        }

    }
}