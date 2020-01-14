using Dating.Common.Models;
using Dating.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dating.DataAccess.Repositories
{
	public class FriendRequestRepository
	{

		public List<FriendRequest> GetFriendRequests(string userId)
		{
			using (var _ctx = new DatingContext())
			{
				var friends = _ctx.Set<FriendRequest>().Where(x => x.FriendRequestUserId == userId && !x.IsFriend).Include(x=>x.Profile);
				return friends.ToList();
			}
		}
		

		public FriendRequest AcceptFriendRequest(long friendRequestId)
		{
			using (var _ctx = new DatingContext())
			{
				var friends = _ctx.Set<FriendRequest>().Where(x => x.Id == friendRequestId);

				var friendRequest = friends.FirstOrDefault();

				friendRequest.IsFriend = true;

				_ctx.Entry(friendRequest).State = EntityState.Modified;

				_ctx.SaveChanges();

				return friendRequest;
			}
		}


		public FriendRequest GetById(long friendRequestId)
		{
			using (var _ctx = new DatingContext())
			{
				var friends = _ctx.Set<FriendRequest>().Where(x => x.Id == friendRequestId);
				
				return friends.FirstOrDefault();
			}
		}

		public bool Exists(string userId, string friendUserId)
		{
			using (var _ctx = new DatingContext())
			{
				var friends = _ctx.Set<FriendRequest>().Where(x => (x.UserId == userId && x.FriendRequestUserId == friendUserId) || (x.UserId == friendUserId && x.FriendRequestUserId == userId));
				return friends.FirstOrDefault() != null;
			}
		}
		public bool AreFriends(string userId, string friendUserId)
		{
			using (var _ctx = new DatingContext())
			{
				var friends = _ctx.Set<FriendRequest>().Where(x => (x.UserId == userId && x.FriendRequestUserId == friendUserId) || (x.UserId == friendUserId && x.FriendRequestUserId == userId));
				return friends.FirstOrDefault() != null && friends.FirstOrDefault().IsFriend;
			}
		}

		public FriendRequest Add(FriendRequest friend)
		{
			using (var _ctx = new DatingContext())
			{
				var foundFriendRequest = _ctx.FriendRequests.Any(x => x.FriendRequestProfileId == friend.FriendRequestProfileId && x.ProfileId == friend.ProfileId);
				if (!foundFriendRequest)
				{
					_ctx.Entry<FriendRequest>(friend).State = System.Data.Entity.EntityState.Added;
					_ctx.SaveChanges();
				}
				return friend;
			}
		}

		public void DeclineFriendRequest(long friendRequestId)
		{
			using (var _ctx = new DatingContext())
			{
				var friendRequest = GetById(friendRequestId);
				if (friendRequest != null)
				{
					_ctx.Entry<FriendRequest>(friendRequest).State = System.Data.Entity.EntityState.Deleted;
					_ctx.SaveChanges();
				}
				
			}
		}
	}
}