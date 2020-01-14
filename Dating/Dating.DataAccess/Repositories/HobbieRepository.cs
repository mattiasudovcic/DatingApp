using Dating.Common.Models;
using Dating.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dating.DataAccess.Repositories
{
	
	public class HobbieRepository
	{
		public List<Hobbie> GetAll()
		{
			using (var _ctx = new DatingContext())
			{
				var hobbies = _ctx.Set<Hobbie>();
				return hobbies.ToList();
			}
		}

	}
}