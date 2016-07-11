using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BLL.Magazine.Models
{
    public class MagazineModel
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "必填")]
        [Display(Name = "期刊名称")]
        public string Name { get; set; }

        public string Year { get; set; }

        public string Period { get; set; }

        public string TotalPeriod { get; set; }

        public string Publish { get; set; }

        public string Council { get; set; }

        public string CopyRight { get; set; }

        public IEnumerable<string> Category { get; set; }

        public string CoverImage { get; set; }


    }
}
