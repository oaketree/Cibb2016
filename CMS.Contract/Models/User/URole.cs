﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.User
{
    [Table("Tbl_UserRole")]
    public class URole
    {
        [Key]
        public int RoleID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{1}到{0}个字")]
        [Display(Name = "名称")]
        public string Name { get; set; }


        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(50, ErrorMessage = "少于{0}个字")]
        [Display(Name = "说明")]
        public string Description { get; set; }

        public virtual ICollection<URoleAuthorise> Authorizes { get; set; }
    }
}
