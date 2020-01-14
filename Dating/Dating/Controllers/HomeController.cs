using Dating.DataAccess.Managers;
using Dating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Dating.Controllers
{
	public class HomeController : Controller
	{
		private readonly ProfileManager _profileManager;
		public HomeController()
		{
			_profileManager = new ProfileManager();
		}

		//Get last five profiles. Recents login.
		public ActionResult Index()
		{
			var profiles = _profileManager.GetFirstFiveProfiles();
			var users = new List<UserViewModel>();
			foreach (var profile in profiles)
			{
				users.Add(new UserViewModel()
				{
					ProfileId = profile.Id,
					UserName = profile.UserName,
					Email = profile.Email,
					ImagePath = profile.ImagePath
				});
			}
			return View(users);
		}

		
	}
}