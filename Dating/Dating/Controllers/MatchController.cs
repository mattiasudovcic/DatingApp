using Dating.DataAccess.Managers;
using Dating.Helpers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dating.Controllers
{
	[Authorize]
	public class MatchController : Controller
	{
		private readonly ProfileManager _profileManager;
		private readonly MatchManager _matchManager;
		public MatchController()
		{
			_profileManager = new ProfileManager();
			_matchManager = new MatchManager();
		}

		//View all potencial matches
		[CustomerAuthorize]
		public ActionResult Index()
		{
			var currentProfile = _profileManager.GetByUserId(User.Identity.GetUserId());
			var matchesProfiles = _matchManager.GetMatchesByProfile(currentProfile);

			return View(matchesProfiles);
		}

		//returns if two profiles are potencial match
		[CustomerAuthorize]
		public JsonResult IsMatch(long friendProfileId)
		{
			var currentProfile = _profileManager.GetByUserId(User.Identity.GetUserId());
			var friendProfile = _profileManager.GetById(friendProfileId);

			var isMatch = _matchManager.IsMatch(currentProfile, friendProfile);

			return Json(isMatch, JsonRequestBehavior.AllowGet);
		}
	}
}