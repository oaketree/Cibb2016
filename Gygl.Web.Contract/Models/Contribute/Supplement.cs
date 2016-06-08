using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gygl.Web.Contract.Models.Contribute
{
    /// <summary>
    ///  补充资料类
    /// </summary>
    [Table("Tbl_Supplement")]
    public class Supplement
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }

        public int Mid { get; set; }

        public string FileUpload { get; set; }

        public STypes SType { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }

        public enum STypes
        {
            Supplement=2,
            Amendment,
        }

    }
}
