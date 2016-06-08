using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gygl.Web.Contract.Models.Contribute
{
    [Table("Tbl_Contribute")]
    public class Contributes
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Author { get; set; }

        public string FileUpload { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Tel { get; set; }

        [StringLength(50)]
        public string ZipCode { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }

        //是否已经提交
        public bool IsSubmit { get; set; }

        //正式稿件编号
        public int MID { get; set; }


        /// <summary>
        /// 注册时间
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }
    }
}
