using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.Magazine
{
    [Table("Tbl_GyglCategoryRelation")]
    public class GyglCategoryRelation
    {
        [Key]
        public int ID { get; set; }

        public int? GyglID { get; set; }

        public int? CategoryID { get; set; }

        [ForeignKey("GyglID")]
        public virtual Gygl Gygl { get; set; }

        [ForeignKey("CategoryID")]
        public virtual GyglCategory Category { get; set; }
    }
}
