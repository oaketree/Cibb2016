using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.Forum
{
    [Table("Tbl_ForumReport")]
    public class Report
    {
        [Key]
        public int ID { get; set; }

        public int? ForumID { get; set; }

        public int? Category { get; set; }

        public int? SubCategory { get; set; }

        public int? Hits { get; set; }

        [StringLength(50, ErrorMessage = "应少于{0}个字")]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = "应少于{0}个字")]
        public string Speaker { get; set; }

        [Column(TypeName = "text")]
        public string Profile { get; set; }

        [Column(TypeName = "text")]
        public string Summary { get; set; }

        public string Image { get; set; }

        public string FileUpload { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }

    }
}
