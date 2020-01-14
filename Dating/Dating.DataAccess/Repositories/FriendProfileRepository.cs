using Dating.Common.Models;
using Dating.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dating.DataAccess.Repositories
{
	public class FriendProfileRepository
	{
		public FriendProfile GetById(long friendProfileId)
		{
			using (var _ctx = new DatingContext())
			{
				var profile = _ctx.Set<FriendProfile>().Where(x => x.Id == friendProfileId);
				return profile.FirstOrDefault();

			}
		}

		public List<FriendProfile> GetAll(long profileId)
		{
			using (var _ctx = new DatingContext())
			{
				var friends = _ctx.Set<FriendProfile>().Where(x => x.ProfileId == profileId && x.FriendUserProfile.Active).Include(x=>x.FriendUserProfile).Include(x=>x.Profile);
				return friends.ToList();
			}
		}

		public List<FriendProfile> GetAllFavourites(long profileId)
		{
			using (var _ctx = new DatingContext())
			{
				var friends = _ctx.Set<FriendProfile>().Where(x => x.ProfileId == profileId && x.FriendUserProfile.Active && x.IsFavourite).Include(x => x.FriendUserProfile).Include(x => x.Profile);
				return friends.ToList();
			}
		}

		public FriendProfile Add(FriendProfile friend)
		{
			using (var _ctx = new DatingContext())
			{
				var foundFriend = _ctx.Set<FriendProfile>().Any(x => x.FriendProfileId == friend.FriendProfileId && x.ProfileId == friend.ProfileId);
				if (!foundFriend)
				{
					_ctx.Entry<FriendProfile>(friend).State = System.Data.Entity.EntityState.Added;
					_ctx.SaveChanges();
				}
				return friend;
			}
		}

		public FriendProfile Update(FriendProfile friend)
		{
			using (var _ctx = new DatingContext())
			{
				_ctx.Entry<FriendProfile>(friend).State = System.Data.Entity.EntityState.Modified;
				_ctx.SaveChanges();
				return friend;
			}
		}

	}
}