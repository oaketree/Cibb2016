using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS.BLL.User.Models
{
    public class RoleAuthModel
    {
        public int RoleID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{1}到{0}个字")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(50, ErrorMessage = "少于{0}个字")]
        [Display(Name = "说明")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// 权限选择
        /// </summary>
        [Display(Name = "所属权限")]
        public IEnumerable<System.Web.Mvc.SelectListItem> Authorises { get; set; }
    }
}
