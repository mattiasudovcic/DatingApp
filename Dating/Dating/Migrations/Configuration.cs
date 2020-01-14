namespace Dating.Migrations
{
	using Dating.Models;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<Dating.Models.ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			AutomaticMigrationDataLossAllowed = true;
			ContextKey = "Dating.Models.ApplicationDbContext";
		}

		protected override void Seed(Dating.Models.ApplicationDbContext context)
		{
			#region roles
			//User role
			if (!context.Roles.Any(r => r.Name == "User"))
			{
				var store = new RoleStore<IdentityRole>(context);
				var manager = new RoleManager<IdentityRole>(store);
				var role = new IdentityRole { Name = "User" };


				manager.Create(role);
			}

			//Admin role
			if (!context.Roles.Any(r => r.Name == "Admin"))
			{
				var store = new RoleStore<IdentityRole>(context);
				var manager = new RoleManager<IdentityRole>(store);
				var role = new IdentityRole { Name = "Admin" };


				manager.Create(role);
			}

			#endregion
			#region users
			//Male profile
			if (!context.Users.Any(u => u.UserName == "john@live.com"))
			{
				var store = new UserStore<ApplicationUser>(context);
				var manager = new UserManager<ApplicationUser>(store);
				var user = new ApplicationUser { UserName = "john@live.com", Email = "john@live.com", EmailConfirmed = true, PhoneNumberConfirmed = true };

				manager.Create(user, "_John123");
				manager.AddToRole(user.Id, "User");
			}

			//Women profile
			if (!context.Users.Any(u => u.UserName == "jessica@live.com"))
			{
				var store = new UserStore<ApplicationUser>(context);
				var manager = new UserManager<ApplicationUser>(store);
				var user = new ApplicationUser { UserName = "jessica@live.com", Email = "jessica@live.com", EmailConfirmed = true, PhoneNumberConfirmed = true };

				manager.Create(user, "_Jessi123");
				manager.AddToRole(user.Id, "User");
			}

			//Bisexual profile
			if (!context.Users.Any(u => u.UserName == "bisexual@live.com"))
			{
				var store = new UserStore<ApplicationUser>(context);
				var manager = new UserManager<ApplicationUser>(store);
				var user = new ApplicationUser { UserName = "bisexual@live.com", Email = "bisexual@live.com", EmailConfirmed = true, PhoneNumberConfirmed = true };

				manager.Create(user, "_Bisexual123");
				manager.AddToRole(user.Id, "User");
			}


			//admin profile
			if (!context.Users.Any(u => u.UserName == "admin@live.com"))
			{
				var store = new UserStore<ApplicationUser>(context);
				var manager = new UserManager<ApplicationUser>(store);
				var user = new ApplicationUser { UserName = "admin@live.com", Email = "admin@live.com", EmailConfirmed = true, PhoneNumberConfirmed = true };

				manager.Create(user, "_Admin123");
				manager.AddToRole(user.Id, "Admin");
			}
			#endregion
		}
	}

}
