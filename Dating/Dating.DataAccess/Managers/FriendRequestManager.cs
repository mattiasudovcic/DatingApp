using Dating.Common.Models;
using Dating.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dating.DataAccess.Managers
{
	public class FriendRequestManager
	{
		private readonly FriendRequestRepository _friendRequestRepository;

		public FriendRequestManager()
		{
			_friendRequestRepository = new FriendRequestRepository();
		}

		//Get all pending friendRequests by user id
		public List<FriendRequest> GetFriendRequests(string userId)
		{
			try
			{
				return _friendRequestRepository.GetFriendRequests(userId);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve all friend requests.");
			}
		}


		//Returns true if two users are friends
		public bool AreFriends(string currentUserId, string friendUserId)
		{
			try
			{
				return _friendRequestRepository.AreFriends(currentUserId, friendUserId);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve the friendship relationship.");
			}
		}

		//Returns true if exists a friendRequest between friends
		public bool Exists(string currentUserId, string friendUserId)
		{
			try
			{
				return _friendRequestRepository.Exists(currentUserId, friendUserId);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve the friendship relationship.");
			}
		}		

		//Allows add a friend's friend request
		public FriendRequest Add(FriendRequest friend)
		{
			try
			{
				return _friendRequestRepository.Add(friend);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to add the user's profile.");
			}
		}

		//Allows accept a friend's friend request
		public FriendRequest AcceptFriendRequest(long friendRequestId)
		{
			try
			{
				return _friendRequestRepository.AcceptFriendRequest(friendRequestId);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve friend request by id.");
			}
		}

		//Allows decline a friend's friend request
		public void DeclineFriendRequest(long friendRequestId)
		{
			try
			{
				_friendRequestRepository.DeclineFriendRequest(friendRequestId);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to remove the user's profile.");
			}
		}
	}
}