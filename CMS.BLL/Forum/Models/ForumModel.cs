using System.ComponentModel.DataAnnotations;

namespace CMS.BLL.Forum.Models
{
    public class ForumModel
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "必填")]
        [Display(Name = "论坛名称")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "论坛简称")]
        public string SimpleName { get; set; }


        [StringLength(50)]
        [Display(Name = "联系人")]
        public string Contacts { get; set; }

        [StringLength(50)]
        [Display(Name = "电话")]
        public string Phone { get; set; }

        [StringLength(50)]
        [Display(Name = "电子邮箱")]
        public string Email { get; set; }

        //[DataType(DataType.Date)]
        public string YearFrom { get; set; }

        //[DataType(DataType.Date)]
        public string YearTo { get; set; }

    }
}
