﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.Magazine
{
    [Table("Tbl_Gygl")]
    public class Gygl
    {
        [Key]
        public int ID { get; set; }

        public int? Year { get; set; }

        public int? Period { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }

        [StringLength(50)]
        public string CoverImage { get; set; }

        public virtual ICollection<GyglCategoryRelation> Category { get; set; }
    }
}
