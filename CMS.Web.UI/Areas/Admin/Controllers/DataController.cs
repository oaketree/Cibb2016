using CMS.BLL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Web.UI.Areas.Admin.Controllers
{
    public class DataController : Controller
    {
        private IWebService webservice;
        public DataController(IWebService webservice)
        {
            this.webservice = webservice;
        }

        // GET: Admin/Data
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Menu()
        {
            return PartialView();
        }

        public ActionResult UserData()
        {
            return View();
        }
        /// <summary>
        /// 显示协会用户密码
        /// </summary>
        /// <returns></returns>
        public string getUser()
        {
            var a = this.webservice.getPasswordXH();
            return a;
        }


        //public string passwordconv()
        //{
        //    return this.webservice.pcv();
        //}

        /// <summary>
        /// 协会密码copy
        /// </summary>
        /// <returns></returns>
        public string passwordXH()
        {
            return this.webservice.PasswordXHCopy();
        }
        /// <summary>
        /// 协会资料copy
        /// </summary>
        /// <returns></returns>
        public string copydetailXH()
        {
            return this.webservice.copyotherXH();
        }


        public string copyother()
        {
            return this.webservice.copyother();
        }

        public string copyperson()
        {
            return this.webservice.copyperson();
        }

        public string changename()
        {
            return this.webservice.changeName();
        }
    }
}