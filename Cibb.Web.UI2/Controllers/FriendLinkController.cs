using Cibb.Web.BLL.FriendLinks;
using System.Web.Mvc;

namespace Cibb.Web.UI2.Controllers
{
    public class FriendLinkController : Controller
    {
        private IWebService webservice;

        public FriendLinkController(IWebService webservice)
        {
            this.webservice = webservice;
        }
        // GET: FriendLink
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult PeopleForHome(int type)
        {
            return PartialView(webservice.GetPeople(type,5));
        }

        public PartialViewResult CompanyForHome()
        {
            return PartialView(webservice.GetCompany(5));
        }
    }
}