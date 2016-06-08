using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.Magazine
{
    [Table("Tbl_GyglAssist")]
    public class GyglAssist
    {
        [Key]
        public int ID { get; set; }

        public int? UserID { get; set; }

       public int? ArticleID { get; set; }

        public bool? Assist { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }
    }
}
