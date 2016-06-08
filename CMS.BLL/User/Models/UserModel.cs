using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CMS.BLL.User.Models
{
    public class UserModel
    {
        public int UserID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "必填")]
        [Display(Name = "用户名")]
        [Remote("CkUserName", "User", ErrorMessage = "{0}已被注册")]
        public string UserName { get; set; }


        [StringLength(50)]
        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "必填")]
        [Display(Name = "密码")]
        public string UserPassword { get; set; }


        [StringLength(50, MinimumLength = 2, ErrorMessage = "{2}到{1}个字符")]
        [Display(Name = "真实姓名")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(50)]
        [Display(Name = "电子邮箱")]
        [RegularExpression(@"(\w)+(\.\w+)*@(\w)+((\.\w+)+)", ErrorMessage = "{0}格式不正确")]  //正则验证
        public string Email { get; set; }


        [DataType(DataType.PhoneNumber, ErrorMessage = "电话号码格式不正确")]
        [Display(Name = "联系电话")]
        public string Tel { get; set; }

        [StringLength(100)]
        [Display(Name = "公司名称")]
        public string CompanyName { get; set; }

        [StringLength(100)]
        [Display(Name = "联系地址")]
        public string Address { get; set; }

        [Display(Name = "所属角色")]
        public IEnumerable<System.Web.Mvc.SelectListItem> Roles { get; set; }

        /// <summary>
        /// 验证列表数据
        /// </summary>
        public List<System.Web.Mvc.SelectListItem> ValidList { get; set; }

        [Display(Name = "验证情况")]
        public string Valid { get; set; }
    }
}
