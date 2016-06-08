using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.Magazine
{
    [Table("Tbl_GyglArticle")]
    public class GyglArticle
    {
        [Key]
        public int ID { get; set; }
        public int? GyglID { get; set; }
        public int? CategoryID { get; set; }
        public int? Hit { get; set; }

        //public int? Year { get; set; }

        //public int? Period { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Author { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        public string Keyword { get; set; }

        public string Summary { get; set; }

        public bool Verify { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }

        [ForeignKey("GyglID")]
        public virtual Gygl Gygl { get; set; }

        [ForeignKey("CategoryID")]
        public virtual GyglCategory Category { get; set; }
    }
}
