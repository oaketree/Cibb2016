using System.ComponentModel.DataAnnotations;

namespace CMS.BLL.Magazine.Models
{
    public class CategoryModel
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "必填")]
        [Display(Name = "分类名称")]
        public string Name { get; set; }
    }
}
