using Dating.Common.Models;
using Dating.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dating.DataAccess.Repositories
{
	public class ProfileRepository
	{
		public List<Profile> GetFirstFiveProfiles()
		{
			using (var _ctx = new DatingContext())
			{
				var profile = _ctx.Set<Profile>().OrderBy(x=>x.CreatedDate).Take(5);
				return profile.ToList();
			}
		}

		public Profile GetByUserName(string userName)
		{
			using (var _ctx = new DatingContext())
			{
				var profile = _ctx.Set<Profile>().Where(x => x.UserName == userName);
				return profile.FirstOrDefault();

			}
		}

		public List<Profile> GetAll()
		{
			using (var _ctx = new DatingContext())
			{
				var profiles = _ctx.Set<Profile>().Where(x=>x.Active).Include(x => x.Hobbies.Select(y => y.Hobbie));
				return profiles.ToList();
			}
		}

		public List<Profile> GetByFilters(string searchString, long? genderTarget)
		{
			using (var _ctx = new DatingContext())
			{
				var profile = _ctx.Set<Profile>().Where(x => x.Active && ((string.IsNullOrEmpty(searchString) || x.UserName.Contains(searchString)) 
														|| (x.FirstName.Contains(searchString)  || x.LastName.Contains(searchString)))
														&& (genderTarget == null || x.GenderId == genderTarget)).Include(x => x.Hobbies);
				return profile.ToList();
			}
		}

		public Profile GetByUserId(string userId)
		{
			using (var _ctx = new DatingContext())
			{
				var profile = _ctx.Set<Profile>().Where(x => x.UserId == userId).Include(x => x.Hobbies);
				return profile.FirstOrDefault();

			}
		}

		public Profile GetById(long profileId)
		{
			using (var _ctx = new DatingContext())
			{
				var profile = _ctx.Set<Profile>().Where(x => x.Id == profileId).Include(x => x.Hobbies);
				return profile.FirstOrDefault();
			}
		}

		public Profile Add(Profile profile)
		{
			using (var _ctx = new DatingContext())
			{				
				_ctx.Entry<Profile>(profile).State = System.Data.Entity.EntityState.Added;
				_ctx.SaveChanges();
				return profile;
			}
		}

		public Profile Update(Profile profile)
		{
			using (var _ctx = new DatingContext())
			{
				var oldProfile = GetByUserId(profile.UserId);
			

				if (profile.Hobbies == null)
				{
					var oldProfileHobbies = oldProfile.Hobbies.ToList();
					foreach (var profileHobbie in oldProfileHobbies)
					{
						profileHobbie.Hobbie = null;
						profileHobbie.Profile = null;
						_ctx.Entry(profileHobbie).State = EntityState.Deleted;
					}
					oldProfile.Hobbies.Clear();
				}
				else
				{
					profile.Hobbies.Where(r => r.Id == 0).ToList().ForEach(item => _ctx.Entry(item).State = EntityState.Added);
					foreach (var item in profile.Hobbies.Where(r => r.Id > 0))
					{
						_ctx.Entry(item).State = EntityState.Modified;
					}
					if (oldProfile != null)
					{
						var oldHobbies = oldProfile.Hobbies.Where(r => !profile.Hobbies.Any(o => r.Id == o.Id)).ToList();
						foreach(var hobbie in oldHobbies)
						{
							hobbie.Profile = null;
							_ctx.Entry(hobbie).State = EntityState.Deleted;
						}
						
					}
				}
				_ctx.Entry(oldProfile).State = EntityState.Detached;
				_ctx.Entry(profile).State = EntityState.Modified;
				_ctx.SaveChanges();
				return profile;
			}
		}

	}
}