using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Contract.Models.Forum
{
     [Table("Tbl_ForumCategory")]
    public class ForumCategory
    {
         [Key]
         public int ID { get; set; }

         public int? ForumID { get; set; }

         public int? Category { get; set; }

         [StringLength(50)]
         public string Name { get; set; }

         [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
         public DateTime? RegDate { get; set; }
    }
}
