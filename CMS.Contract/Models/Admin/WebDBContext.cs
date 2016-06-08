using Cibb.Core.Cache;
using Cibb.Core.DAL;
using System.Data.Entity;

namespace CMS.Contract.Models.Admin
{
    public class WebDBContext : DbContextBase
    {
        public WebDBContext()
            : base(CacheService.GetConn("CMS"))
        {

        }
        public virtual DbSet<Admins> Admin { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<AdminRole> AdminRole { get; set; }
        public virtual DbSet<Authorise> Authorise { get; set; }
        public virtual DbSet<RoleAuthorise> RoleAuthorise { get; set; }
    }
}
