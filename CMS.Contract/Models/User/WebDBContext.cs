using Cibb.Core.Cache;
using Cibb.Core.DAL;
using System.Data.Entity;

namespace CMS.Contract.Models.User
{
    public class WebDBContext : DbContextBase
    {
        public WebDBContext()
            : base(CacheService.GetConn("CMS"))
        {

        }
        public virtual DbSet<Users> User { get; set; }
        public virtual DbSet<URole> URole { get; set; }
        public virtual DbSet<UAuthorise> UAuthorise { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<URoleAuthorise> URoleAuthorise { get; set; }
        public virtual DbSet<UserDetail> UserDetail { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Person> Person { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new DestinationMap());
            modelBuilder.Entity<UserDetail>().HasRequired(n => n.Users).WithOptional(n => n.UserDetail);

        }
    }
}
