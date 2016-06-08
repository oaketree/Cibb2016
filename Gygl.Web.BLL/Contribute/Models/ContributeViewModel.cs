using System.ComponentModel.DataAnnotations;

namespace Gygl.Web.BLL.Contribute.Models
{
    public class ContributeViewModel
    {
        [StringLength(50, MinimumLength = 5, ErrorMessage = "{2}到{1}个字符")]
        [Required(ErrorMessage = "文章名为必填项目")]
        public string Title { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "作者为必填项目")]
        public string Author { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "公司名为必填项目")]
        public string Company { get; set; }


        [StringLength(200)]
        [Required(ErrorMessage = "地址为必填项目")]
        public string Address { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "电子邮箱为必填项目")]
        [Display(Name = "电子邮箱")]
        [RegularExpression(@"(\w)+(\.\w+)*@(\w)+((\.\w+)+)", ErrorMessage = "{0}格式不正确")]  //正则验证
        public string Email { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "电话为必填项目")]
        public string Tel { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "邮编为必填项目")]
        [Display(Name = "邮编")]
        [RegularExpression(@"\d{6}", ErrorMessage = "{0}格式不正确")]  //正则验证
        public string ZipCode { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }

        public string FileUpload { get; set; }
    }
}
