using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cibb.Web.Contract.Model
{
    [Table("Tbl_News_Column")]
    public partial class NewsColumn
    {
        [Key]
        public int ColumnID { get; set; }

        public int? ColumnParentID { get; set; }

        [StringLength(50)]
        public string ColumnName { get; set; }
    }
}
