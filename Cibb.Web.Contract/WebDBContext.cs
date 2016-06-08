using Cibb.Core.Cache;
using Cibb.Core.DAL;
using Cibb.Web.Contract.Model;
using System.Data.Entity;

namespace Cibb.Web.Contract
{
    public class WebDBContext : DbContextBase
    {
         public WebDBContext()
            : base(CacheService.GetConn("CibbContext1"))
        {

        }
         public virtual DbSet<New> News { get; set; }
         public virtual DbSet<NewsColumn> NewsColumns { get; set; }
         public virtual DbSet<BranchDirector> BranchDirectors { get; set; }
         public virtual DbSet<BranchMember> BranchMembers { get; set; }
         public virtual DbSet<FriendLink> FriendLinks { get; set; }

         public virtual DbSet<Users> User { get; set; }
        public virtual DbSet<UserDetail> UserDetail { get; set; }
        public virtual DbSet<Role> Role { get; set; }
         public virtual DbSet<Authorise> Authorise { get; set; }
         public virtual DbSet<UserRole> UserRole { get; set; }
         public virtual DbSet<RoleAuthorise> RoleAuthorise { get; set; }
         public virtual DbSet<Forums> Forum { get; set; }
         public virtual DbSet<ForumCategory> ForumCategory { get; set; }
         public virtual DbSet<Report> Report { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new DestinationMap());
            modelBuilder.Entity<UserDetail>().HasRequired(n => n.Users).WithOptional(n => n.UserDetail);

        }

    }
}
