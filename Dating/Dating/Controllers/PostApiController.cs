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

	[RoutePrefix("api/Posts")]
	public class PostApiController : ApiController
	{

		private readonly ProfileManager _profileManager;
		private readonly ProfilePostManager _profilePostManager;

		public PostApiController()
		{
			_profileManager = new ProfileManager();
			_profilePostManager = new ProfilePostManager();
		}

		[Route("post/add")]
		[HttpGet]
		public void Add(long profileId, long friendProfileId, string message)
		{

			_profilePostManager.Add(new ProfilePost
			{
				Date = DateTime.Now,
				ProfileId = profileId,
				FriendProfileId = friendProfileId,
				Message = message
			});

		}


		[Route("post/remove")]
		[HttpGet]
		public void Remove(long profilePostId)
		{			
			_profilePostManager.Remove(profilePostId);

		}
	}


	


}
