using Cibb.Core.Cache;
using Cibb.Core.DAL;
using System.Data.Entity;

namespace CMS.Contract.Models.Forum
{
    public class WebDBContext : DbContextBase
    {
        public WebDBContext()
            : base(CacheService.GetConn("CMS"))
        {

        }
        public virtual DbSet<Forums> Forum { get; set; }
        public virtual DbSet<ForumCategory> ForumCategory { get; set; }
        public virtual DbSet<Report> Report { get; set; }

    }
}
