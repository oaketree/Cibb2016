using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gygl.Web.Contract.Models.Contribute
{
    /// <summary>
    /// 审稿信息
    /// </summary>
    [Table("Tbl_M_Message")]
    public class Message
    {
        [Key]
        public int id { get; set; }

        public string Mid { get; set; }

        public string message { get; set; }

        public MType mtype { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? Regdate { get; set; }

    }

    public enum MType
    {
        Normal=1,
        Supplement,
        Amendment,

    }
}
