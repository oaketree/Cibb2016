using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cibb.Web.Contract.Model
{
    [Table("Tbl_expert")]
    public partial class BranchDirector
    {
        [Key]
        public int expertid { get; set; }

        [StringLength(50)]
        public string truename { get; set; }

        [StringLength(50)]
        public string duty { get; set; }

        [StringLength(100)]
        public string companyduty { get; set; }

        [Column(TypeName = "text")]
        public string resume { get; set; }

        [StringLength(50)]
        public string expertpic { get; set; }

        public int? sortid { get; set; }
    }
}
