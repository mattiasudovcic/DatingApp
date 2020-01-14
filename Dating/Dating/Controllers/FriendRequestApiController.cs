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
	[RoutePrefix("api/FriendRequests")]
	public class FriendRequestApiController : ApiController
	{

		//Add a friend request between friends.
		[Route("FriendRequest/add")]
		[HttpGet]
		public void AddFriend(string friendsId)
		{
			var friendRequestManager = new FriendRequestManager();
			var profileManager = new ProfileManager();
			var currentUserId = User.Identity.GetUserId();
			var currentProfile = profileManager.GetByUserId(currentUserId);

			var friendProfile = profileManager.GetByUserId(friendsId);
			var areFriend = friendRequestManager.AreFriends(currentUserId, friendsId);

			if (!areFriend)
			{
				var friend = new FriendRequest
				{
					UserId = currentUserId,
					ProfileId = currentProfile.Id,
					FriendRequestUserId = friendsId,
					FriendRequestProfileId = friendProfile.Id,				
					IsFriend = false,

				};

				friendRequestManager.Add(friend);



			}
		}

		//Accept a friend request 
		[Route("FriendRequest/accept")]
		[HttpGet]
		public void AcceptFriend(long friendRequestId)
		{
			var friendRequestManager = new FriendRequestManager();
			var friendProfileManager = new FriendProfileManager();
			var friendRequest = friendRequestManager.AcceptFriendRequest(friendRequestId);

			friendProfileManager.Add(new FriendProfile()
			{
				ProfileId = friendRequest.ProfileId,
				FriendProfileId = friendRequest.FriendRequestProfileId,
				IsFavourite = false

			});

			friendProfileManager.Add(new FriendProfile()
			{
				ProfileId = friendRequest.FriendRequestProfileId,
				FriendProfileId = friendRequest.ProfileId,
				IsFavourite = false

			});
		}

		//Decline a friend request 
		[Route("FriendRequest/decline")]
		[HttpGet]
		public void DeclineFriend(long friendRequestId)
		{
			var friendRequestManager = new FriendRequestManager();

			friendRequestManager.DeclineFriendRequest(friendRequestId);


		}
	}
}
