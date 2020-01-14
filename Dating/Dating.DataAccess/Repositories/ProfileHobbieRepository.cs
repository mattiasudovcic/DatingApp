using Dating.Common.Models;
using Dating.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dating.DataAccess.Repositories
{
	public class ProfileHobbieRepository
	{
		public virtual void Remove(ProfileHobbie profileHobbie)
		{
			using (var _ctx = new DatingContext())
			{
				_ctx.Entry(profileHobbie).State = EntityState.Deleted;

			}
		}

	}
}