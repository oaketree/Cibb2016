using Cibb.Web.BLL.Reports.Models;
using System.Collections.Generic;

namespace Cibb.Web.BLL.Reports
{
    public interface IWebService
    {
        ReportModel GetReportList(int forumid, int cid);
        ReportDetail getReportDetail(int id);
        List<PreviousModel> getPrevious();
        List<LinkTitle> getPreviousList(int forumid, int category);
    }
}
