using Dating.Common.Models;
using Dating.DataAccess.Migrations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Dating.DataAccess.Context
{
	public class DatingContext : DbContext
	{
		public DbSet<Profile> Profiles { get; set; }
		public DbSet<Hobbie> Hobbies { get; set; }
		public DbSet<ProfileHobbie> ProfileHobbies { get; set; }
		public DbSet<FriendProfile> Friends { get; set; }
		public DbSet<FriendRequest> FriendRequests { get; set; }
		public DbSet<VisitorProfile> VisitorsProfile { get; set; }
		public DbSet<ProfilePost> ProfilePosts { get; set; }


		public DatingContext() : base("name=DefaultConnection")
		{
			//Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatingContext, Configuration>("DefaultConnection"));
			Configuration.ProxyCreationEnabled = false;
		}


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
		}

	}
}