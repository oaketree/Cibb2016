using System.ComponentModel.DataAnnotations;

namespace CMS.BLL.Magazine.Models
{
    public class ArticleModel
    {
        public int ID { get; set; }

        public string guid { get; set; }

        /// <summary>
        /// 论坛列表
        /// </summary>
        [Required(ErrorMessage = "请选择期刊")]
        [Display(Name = "期刊名称")]
        public string Gygl { get; set; }

        public string Year { get; set; }

        public string Period { get; set; }


        /// <summary>
        /// 分类列表
        /// </summary>
        [Required(ErrorMessage = "请选择分类")]
        [Display(Name = "分类列表")]
        public string Category { get; set; }

        [StringLength(50, ErrorMessage = "应少于{0}个字")]
        [Required(ErrorMessage = "必填")]
        [Display(Name = "文章题目")]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = "应少于{0}个字")]
        [Display(Name = "作者")]
        public string Author { get; set; }

        [StringLength(100, ErrorMessage = "应少于{0}个字")]
        [Display(Name = "地址")]
        public string Address { get; set; }

        [Display(Name = "关键字")]
        public string Keyword { get; set; }

        [Display(Name = "摘要")]
        public string Summary { get; set; }

        [Display(Name = "权限验证")]
        public string Verify { get; set; }

    }
}
