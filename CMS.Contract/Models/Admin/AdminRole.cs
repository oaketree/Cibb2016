using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.Admin
{
    [Table("Tbl_AdminRoleRelation")]
    public class AdminRole
    {
        [Key]
        public int RelationID { get; set; }
        public int AdminID { get; set; }
        public int RoleID { get; set; }

        public virtual Admins Admin { get; set; }
        public virtual Role Role { get; set; }
    }
}
