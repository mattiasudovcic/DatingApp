using Dating.Common.Models;
using Dating.DataAccess.Managers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dating.Helpers
{
	//Allows redirecting to the profile registration view if the user does not have a profile created.
	public class CustomerAuthorize : AuthorizeAttribute
	{
		private readonly ProfileManager _profileManager = new ProfileManager();
		
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			var profile = _profileManager.GetByUserId(filterContext.HttpContext.User.Identity.GetUserId());

			// If they are authorized, handle accordingly
			if (this.AuthorizeCore(filterContext.HttpContext) && profile != null)
			{
				base.OnAuthorization(filterContext);
			}
			else
			{
				// Otherwise redirect to your specific authorized area
				filterContext.Result = new RedirectResult("~/Profile/Create");
			}
		}
	}
}