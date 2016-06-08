using Cibb.Core.Cache;
using Cibb.Core.DAL;
using System.Data.Entity;

namespace CMS.Contract.Models.Magazine
{
    public class WebDBContext : DbContextBase
    {
        public WebDBContext()
            : base(CacheService.GetConn("CMS"))
        {

        }

        public virtual DbSet<GyglArticle> Article { get; set; }
        public virtual DbSet<GyglCategory> Category { get; set; }
        public virtual DbSet<Gygl> Gygl { get; set; }
        public virtual DbSet<GyglAssist> GyglAssist { get; set; }
        public virtual DbSet<GyglCategoryRelation> GyglCategory { get; set; }
        public virtual DbSet<GyglComment> GyglComment { get; set; }
        public virtual DbSet<GyglImage> GyglImage { get; set; }
    }
}
