using Cibb.Web.BLL.Council;
using System.Web.Mvc;

namespace Cibb.Web.UI2.Controllers
{
    public class CouncilController : Controller
    {
        // GET: Council
        private IWebService webservice;

        public CouncilController(IWebService webservice)
        {
            this.webservice = webservice;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BranchDirector(int page = 1) 
        {
            return View(webservice.GetBranchDirector(7,page));
        }

        public ActionResult BranchCompany()
        {
            return View(webservice.GetBranchCompany());
        }

        public ActionResult BranchMember(int page = 1)
        {
            return View(webservice.GetBranchMember(40,page));
        }

        public ActionResult CouncilDetail(int id)
        {
            var bm = webservice.GetCompanyDetail(id);
            if (bm != null)
                return View(bm);
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult BranchComapnyDetail(int id)
        {
            var bm = webservice.GetCompanyDetail(id);
            if (bm != null)
                return View(bm);
            else
                return RedirectToAction("Index", "Home");
        }
        public ActionResult Introduction()
        {
            return View();
        }

        public ActionResult Regulations()
        {
            return View();
        }

        public ActionResult MembershipManagement()
        {
            return View();
        }

        public ActionResult MembershipFeeManagement()
        {
            return View();
        }

        public ActionResult Institution()
        {
            return View();
        }

        public ActionResult Convention()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult ShenXin()
        {
            return View();
        }

        public ActionResult LiuZhenDuo()
        {
            return View();
        }

        public ActionResult WangShanWu()
        {
            return View();
        }
        public PartialViewResult Left()
        {
            return PartialView();
        }
    }
}