using Cibb.Core.Cache;
using Cibb.Core.DAL;
using System.Data.Entity;

namespace Gygl.Web.Contract.Models.Contribute
{
    public class WebDBContext : DbContextBase
    {
        public WebDBContext()
            : base(CacheService.GetConn("CMS"))
        {

        }
        public virtual DbSet<Contributes> Contributes { get; set; }
        public virtual DbSet<Manuscript> Manuscript { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Supplement> Supplement { get; set; }

    }
}
