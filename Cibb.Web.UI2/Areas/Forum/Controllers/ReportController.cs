using Cibb.Core.Utility;
using Cibb.Web.BLL.Attributes;
using Cibb.Web.BLL.Reports;
using System.Web.Mvc;

namespace Cibb.Web.UI2.Areas.Forum.Controllers
{
    public class ReportController : Controller
    {
        private IWebService webservice;
        private const int forumid = 6;

        public ReportController(IWebService webservice)
        {
            this.webservice = webservice;
        }

        public PartialViewResult ReportList(int cid)
        {
            var a = this.webservice.GetReportList(forumid, cid);
            return PartialView(a);
        }

        public PartialViewResult ReportListDetail(int cid)
        {
            var a = this.webservice.GetReportList(forumid, cid);
            return PartialView(a);
        }

        public ActionResult Detail(int id)
        {
            var d = this.webservice.getReportDetail(id);
            if (d != null)
                return View(d);
            else
                return RedirectToAction("Previous");
        }

        public ActionResult Previous()
        {
            return View(this.webservice.getPrevious());
        }

        public PartialViewResult PreviousList(int forumid,int category)
        {
            return PartialView(this.webservice.getPreviousList(forumid,category));
        }

        [Permission(authorize ="3")]
        public ActionResult GetReportFile(string name)
        {
            if (!string.IsNullOrEmpty(name))
                return File(FileHelper.GetFile(name), "application/octet-stream", name);
            else
                return Content("<script>alert('文件不存在！');window.close();</script>");
        }
    }
}