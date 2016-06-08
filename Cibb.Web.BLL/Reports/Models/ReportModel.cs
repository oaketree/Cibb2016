using System.Collections.Generic;

namespace Cibb.Web.BLL.Reports.Models
{
    public class ReportModel
    {
        public List<ReportListModel> lr { get; set; }
        public ReportListModel rl { get; set; }
        public bool hasubcategory { get; set; }
    }
}
