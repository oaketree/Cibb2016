using System.Linq;

namespace Cibb.Web.BLL.Reports.Models
{
    public class ReportListModel
    {
        //public int subcategoryid { get; set; }
        public string subcategory { get; set; }
        public IQueryable<LinkTitle> title { get; set; }
    }
}
