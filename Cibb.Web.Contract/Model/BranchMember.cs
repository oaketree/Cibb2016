using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cibb.Web.Contract.Model
{
    [Table("Tbl_CompanyFriendLink")]
    public partial class BranchMember
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        [StringLength(150)]
        public string CompanyLink { get; set; }

        [Column(TypeName = "text")]
        public string CompanySummary { get; set; }

        [StringLength(50)]
        public string LinkTel { get; set; }

        [StringLength(200)]
        public string LinkAddress { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public DateTime? Regdate { get; set; }

        public byte? type { get; set; }

        public int? sortid { get; set; }

        public int? province { get; set; }
    }
}
