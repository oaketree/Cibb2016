using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS.BLL.Forum.Models
{
    public class CategoryModel
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "必填")]
        [Display(Name = "分类名称")]
        public string Name { get; set; }

        /// <summary>
        /// 论坛列表
        /// </summary>
        public List<System.Web.Mvc.SelectListItem> ForumList { get; set; }

        [Display(Name = "论坛名称")]
        public string Forum { get; set; }

        /// <summary>
        /// 主分类列表
        /// </summary>
        public List<System.Web.Mvc.SelectListItem> CategoryList { get; set; }

        [Display(Name = "主分类列表")]
        public string Category { get; set; }

    }
}
