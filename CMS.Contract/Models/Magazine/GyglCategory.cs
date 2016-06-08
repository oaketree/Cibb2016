using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.Magazine
{
    [Table("Tbl_GyglCategory")]
    public class GyglCategory
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? SortID { get; set; }
    }
}
