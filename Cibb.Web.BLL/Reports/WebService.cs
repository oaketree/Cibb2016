using Cibb.Core.DAL;
using Cibb.Web.BLL.Reports.Models;
using Cibb.Web.Contract.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibb.Web.BLL.Reports
{
    public class WebService : IWebService
    {
        private IWebRepository<Report> report;
        private IWebRepository<ForumCategory> fc;
        private IWebRepository<Forums> forum;
        public WebService(IWebRepository<Report> report, IWebRepository<ForumCategory> fc, IWebRepository<Forums> forum)
        {
            this.report = report;
            this.fc = fc;
            this.forum = forum;
        }

        public ReportModel GetReportList(int forumid,int cid)
        {
            Func<int, IQueryable<LinkTitle>> cv = c =>
            {
                var r = this.report.FindAll(n => n.ForumID == forumid && n.SubCategory == c).Select(s => new LinkTitle { 
                title=s.Title,
                linkid=s.ID,
                speaker=s.Speaker,
                img=s.Image,
                summary=s.Summary
                });
                return r;
            };

            Func<int, IQueryable<LinkTitle>> cv2 = c =>
            {
                var r = this.report.FindAll(n => n.ForumID == forumid && n.Category == cid).Select(s => new LinkTitle {
                    title = s.Title,
                    linkid = s.ID,
                    speaker = s.Speaker,
                    img=s.Image,
                    summary=s.Summary
                });
                return r;
            };
            ReportModel rm = new ReportModel();
            List<ReportListModel> lr = new List<ReportListModel>();
            var sc = this.fc.FindAll(n => n.ForumID == forumid && n.Category == cid);
            if (sc.Count() > 0)
            {
                rm.hasubcategory = true;
                foreach (var item in sc)
                {
                    lr.Add(new ReportListModel{
                    subcategory = item.Name,
                    title = cv(item.ID),
                });
                }
                rm.lr = lr;
            }
            else
            {
                rm.hasubcategory = false;
                rm.rl = new ReportListModel{
                    title = cv2(cid)
                };
            }
            return rm;
        }

        public ReportDetail getReportDetail(int id)
        {
            var r=this.report.GetById(id);
            if (r != null)
            {
                r.Hits = r.Hits + 1;
                this.report.Update(r);

                return new ReportDetail
                {
                    Title = r.Title,
                    Speaker = r.Speaker,
                    Profile = r.Profile,
                    Summary = r.Summary,
                    Image = r.Image,
                    FileUpload = r.FileUpload
                };
            }
            else
                return null;
        }

        public List<PreviousModel> getPrevious()
        {
            var f = forum.FindList(null, o => o.RegDate, 0).Select(s=>new PreviousModel {
                ForumID=s.ID,
                ForumName=s.Name,
                ForumSimpleName=s.SimpleName
            });
            return f.ToList();
        }

        public List<LinkTitle> getPreviousList(int forumid, int category)
        {
            var r = report.FindAll(n => n.ForumID == forumid && n.Category == category).Select(s=>new LinkTitle {
                title=s.Title,
                linkid=s.ID,
                speaker=s.Speaker,
            });
            return r.ToList();
        }
    }
}
