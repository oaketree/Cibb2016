using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.Magazine
{
    [Table("Tbl_GyglImage")]
    public class GyglImage
    {
        [Key]
        public int ID { get; set; }

        public int ArticleID { get; set; }

        [StringLength(50)]
        public string ImageID { get; set; }

        public int? SortID { get; set; }

        [StringLength(50)]
        public string Guid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }

    }
}
