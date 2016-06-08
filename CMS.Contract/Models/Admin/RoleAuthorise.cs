using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.Admin
{
    [Table("Tbl_AdminRoleAuthoriseRelation")]
    public class RoleAuthorise
    {
        [Key]
        public int RelationID { get; set; }
        public int RoleID { get; set; }
        public int AuthoriseID { get; set; }

        public virtual Authorise Authorise { get; set; }
        public virtual Role Role { get; set; }
    }
}
