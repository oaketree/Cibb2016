using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gygl.Web.Contract.Models.Contribute
{
    [Table("Tbl_Manuscript")]
    public class Manuscript
    {
        [Key]
        public int id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        [StringLength(50)]
        public string filename { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Zipcode { get; set; }

        [StringLength(100)]
        public string Tel { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public int nh { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? Regdate { get; set; }


    }
}
