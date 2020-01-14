using Dating.Common.Models;
using Dating.DataAccess.Managers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dating.Controllers
{
	[RoutePrefix("api/Visitors")]
	public class VisitorApiController : ApiController
	{
		
		[Route("Visitor/getLast")]
		[HttpGet]
		public IEnumerable<Profile> GetLastFiveVisitor()
		{
			var visitorProfileManager = new VisitorProfileManager();
			var profileManager = new ProfileManager();
			var currentUserId = User.Identity.GetUserId();
			var currentProfile = profileManager.GetByUserId(currentUserId);


			return visitorProfileManager.GetLastFiveVisitors(currentProfile.Id);

		}

	}
}
