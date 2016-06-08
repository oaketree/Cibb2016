using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cibb.Web.Contract.Model
{
    [Table("Tbl_News")]
    public partial class New
    {
        [Key]
        public int newsid { get; set; }

        public int ColumnID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        public int? Hits { get; set; }

        public DateTime? RegDate { get; set; }

        [StringLength(200)]
        public string InfoFrom { get; set; }

        public int? userid { get; set; }

        [StringLength(100)]
        public string subTitle { get; set; }

        [StringLength(50)]
        public string upload { get; set; }

        [StringLength(50)]
        public string uploadname { get; set; }

        [StringLength(50)]
        public string upload2 { get; set; }

        [StringLength(50)]
        public string uploadname2 { get; set; }

        [StringLength(50)]
        public string pic { get; set; }

        [StringLength(50)]
        public string Provider { get; set; }

        [StringLength(50)]
        public string video { get; set; }

        [StringLength(100)]
        public string videoname { get; set; }

        [StringLength(50)]
        public string category { get; set; }

        [StringLength(50)]
        public string category2 { get; set; }

        public int? picarea { get; set; }

        public virtual NewsColumn NewsColumn { get; set; }

    }
}
