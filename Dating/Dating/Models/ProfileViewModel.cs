using Dating.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dating.Models
{
	public class ProfileViewModel
	{
		public Profile Profile { get; set; }
		public List<FriendProfile> Friends { get; set; }
		public List<ProfilePost> Posts { get; set; }

		public ProfileViewModel()
		{
			Profile = new Profile();
			Friends = new List<FriendProfile>();
			Posts = new List<ProfilePost>();
		}

	}
}