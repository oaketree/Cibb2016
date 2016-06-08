using System;

namespace Cibb.Web.BLL.Model
{
    public class Link
    {
        public string title { get; set; }
        public int linkid { get; set; }
        public string columnName { get; set; }
        public int columnId { get; set; }
        public DateTime? date { get; set; }
        public string pic { get; set; }
    }
}
