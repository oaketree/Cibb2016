using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.User
{
    [Table("Tbl_Company")]
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }

        public int UserID { get; set; }
        [StringLength(100)]
        public string CompanyName { get; set; }
        [StringLength(50)]
        public string CompanyEmail { get; set; }
        [StringLength(200)]
        public string CompanyAddress { get; set; }
        [StringLength(100)]
        public string CompanyPhone { get; set; }

        [StringLength(100)]
        public string Delegate { get; set; }

        [StringLength(100)]
        public string CompanyPeople { get; set; }

        [StringLength(100)]
        public string QQ { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CompanyRegDate { get; set; }



    }
}
