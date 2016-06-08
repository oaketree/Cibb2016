using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CMS.BLL.Forum.Models
{
    public class ArticleModel
    {
        public int ID { get; set; }

        /// <summary>
        /// 论坛列表
        /// </summary>
        [Required(ErrorMessage = "请选择论坛")]
        [Display(Name = "论坛名称")]
        public string Forum { get; set; }

        /// <summary>
        /// 主分类列表
        /// </summary>
        [Required(ErrorMessage = "请选择主分类")]
        [Display(Name = "主分类列表")]
        public string Category { get; set; }


        /// <summary>
        /// 次要分类列表
        /// </summary>
        [Display(Name = "次要分类列表")]
        public string SubCategory { get; set; }

        [StringLength(50, ErrorMessage = "应少于{0}个字")]
        [Required(ErrorMessage = "必填")]
        [Display(Name = "报告题目")]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = "应少于{0}个字")]
        [Display(Name = "演讲人")]
        public string Speaker { get; set; }

        [Display(Name = "演讲人介绍")]
        public string Profile { get; set; }

        [Display(Name = "报告摘要")]
        public string Summary { get; set; }

        public string Image { get; set; }

        public string FileUpload { get; set; }
    }
}
