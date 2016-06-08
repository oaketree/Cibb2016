using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CMS.BLL.Admin.Models
{
    public class AdminModel
    {
        public int AdminID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{2}到{1}个字符")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "必填")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{2}到{1}个字符")]
        [Display(Name = "真实姓名")]
        public string DisplayName { get; set; }


        [Display(Name = "密码")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{2}到{1}个字符")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        /// <summary>
        /// 角色选择
        /// </summary>
        [Required(ErrorMessage = "必须选择角色")]
        [Display(Name = "所属角色")]
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
