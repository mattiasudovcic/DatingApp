using Dating.Common.Models;
using Dating.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dating.DataAccess.Repositories
{
	public class VisitorProfileRepository
	{		
		public List<Profile> GetLastFiveVisitors(long profileId)
		{
			using (var _ctx = new DatingContext())
			{
				var friends = _ctx.Set<VisitorProfile>().Where(x => x.ProfileId == profileId && x.FriendUserProfile.Active)
														.OrderByDescending(x=>x.Date).Take(5).Select(x => x.FriendUserProfile).GroupBy(p => p.Id).Select(g => g.FirstOrDefault());
				return friends.ToList();
			}
		}

		public VisitorProfile Add(VisitorProfile visitorProfile)
		{
			using (var _ctx = new DatingContext())
			{
				_ctx.Entry<VisitorProfile>(visitorProfile).State = System.Data.Entity.EntityState.Added;
				_ctx.SaveChanges();
				return visitorProfile;
			}
		}


	}
}