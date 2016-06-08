using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cibb.Web.Contract.Model
{
    [Table("Tbl_FriendLink")]
    public partial class FriendLink
    {
        [Key]
        public int LINKID { get; set; }

        [StringLength(50)]
        public string LinkType { get; set; }

        [StringLength(50)]
        public string LINKNAME { get; set; }

        [StringLength(50)]
        public string LINKNAME2 { get; set; }

        [StringLength(200)]
        public string LINKURL { get; set; }

        [Column(TypeName = "ntext")]
        public string LinkContent { get; set; }

        [StringLength(50)]
        public string LinkPic { get; set; }

        [StringLength(50)]
        public string LinkThumb { get; set; }

        public int? LinkVote { get; set; }

        public int? SortID { get; set; }

        public DateTime? Regdate { get; set; }

        public bool? updated { get; set; }
    }
}
