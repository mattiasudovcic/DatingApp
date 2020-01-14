
using Dating.Common.Models;
using Dating.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dating.DataAccess.Repositories
{
	public class ProfilePostRepository
	{
		public List<ProfilePost> GetAll(long profileId)
		{
			using (var _ctx = new DatingContext())
			{
				var friends = _ctx.Set<ProfilePost>().Where(x => x.ProfileId == profileId).OrderByDescending(x=>x.Date).Include(x => x.FriendUserProfile).Include(x => x.Profile);
				return friends.ToList();
			}
		}

		public ProfilePost Add(ProfilePost profilePost)
		{
			using (var _ctx = new DatingContext())
			{
				_ctx.Entry<ProfilePost>(profilePost).State = System.Data.Entity.EntityState.Added;
				_ctx.SaveChanges();

				return profilePost;
			}
		}

		public ProfilePost Update(ProfilePost profilePost)
		{
			using (var _ctx = new DatingContext())
			{
				_ctx.Entry<ProfilePost>(profilePost).State = System.Data.Entity.EntityState.Modified;
				_ctx.SaveChanges();
				return profilePost;
			}
		}

		public void Remove(long? profilePostId)
		{
			using (var _ctx = new DatingContext())
			{
				var profilePost = _ctx.Set<ProfilePost>().Where(x => x.Id == profilePostId).FirstOrDefault();
				_ctx.Entry<ProfilePost>(profilePost).State = System.Data.Entity.EntityState.Deleted;
				_ctx.SaveChanges();

			}
		}

	}
}