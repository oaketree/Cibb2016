using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.User
{
    [Table("Tbl_UserRoleRelation")]
    public class UserRole
    {
        [Key]
        public int RelationID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public virtual Users User { get; set; }
        public virtual URole Role { get; set; }
    }

    /// <summary>
    /// 细粒度权限控制(拥有相同权限下的程度)
    /// </summary>
    //public enum UserLeveL
    //{
    //    //初级会员
    //    primary,
    //    middle,
    //    advanced,
    //}
}
