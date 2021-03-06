﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cibb.Web.Contract.Model
{
    [Table("Tbl_UserRoleRelation")]
    public class UserRole
    {
        [Key]
        public int RelationID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public virtual Users User { get; set; }
        public virtual Role Role { get; set; }
    }
}
