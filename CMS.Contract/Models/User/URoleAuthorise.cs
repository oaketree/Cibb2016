using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.User
{
    [Table("Tbl_UserRoleAuthoriseRelation")]
    public class URoleAuthorise
    {
        [Key]
        public int RelationID { get; set; }
        public int RoleID { get; set; }
        public int AuthoriseID { get; set; }

        public virtual UAuthorise Authorise { get; set; }
        public virtual URole Role { get; set; }
    }
}
