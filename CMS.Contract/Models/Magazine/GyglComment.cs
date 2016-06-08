using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.Magazine
{
    [Table("Tbl_GyglComment")]
    public class GyglComment
    {
        [Key]
        public int ID { get; set; }

        public int? UserID { get; set; }

        public int? ArticleID { get; set; }

        [StringLength(250)]
        public string Comment { get; set; }

        public int? ReplyID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }


    }
}
