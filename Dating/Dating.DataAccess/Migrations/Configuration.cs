using Dating.Common.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Dating.DataAccess.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<Dating.DataAccess.Context.DatingContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;			
			MigrationsDirectory = @"_SQL\Migrations";
		}


		protected override void Seed(Context.DatingContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//


			//if (context.Profiles.Count() == 0)
			//{
			//	var profiles = new List<Profile>
			//	{
			//		new Profile { Id=1, FirstName = "Jhon" }

			//	};
			//	profiles.ForEach(x => context.Profiles.AddOrUpdate(x));

			//}


			if (context.Hobbies.Count() == 0)
			{
				var hobbies = new List<Hobbie>
				{					
					new Hobbie { Id=1, Name = "Astronomy" },
					new Hobbie { Id=2, Name = "Building" },
					new Hobbie { Id=3, Name = "Computer programming" },
					new Hobbie { Id=4, Name = "Dance" },
					new Hobbie { Id=5, Name = "Drawing" },
					new Hobbie { Id=6, Name = "Fashion design" },
					new Hobbie { Id=7, Name = "Ice skating" },
					new Hobbie { Id=8, Name = "Karaoke" },
					new Hobbie { Id=9, Name = "Karate" },
					new Hobbie { Id=10, Name = "Magic" },
					new Hobbie { Id=11, Name = "Makeup" },
					new Hobbie { Id=12, Name = "Photography" },
					new Hobbie { Id=13, Name = "Puzzles" },
					new Hobbie { Id=14, Name = "Quizzes" },
					new Hobbie { Id=15, Name = "Watching movies" },
					new Hobbie { Id=16, Name = "Yoga" },

				};
				hobbies.ForEach(x => context.Hobbies.AddOrUpdate(x));

			}


		}
	}
}
