using Dating.Common.Models;
using Dating.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dating.DataAccess.Managers
{
	public class FriendProfileManager
	{
		private readonly FriendProfileRepository _friendRepository;

		public FriendProfileManager()
		{
			_friendRepository = new FriendProfileRepository();
		}

		//Get a friendProfile by profile id
		public FriendProfile GetById(long friendProfileId)
		{
			try
			{
				return _friendRepository.GetById(friendProfileId);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve a friend profile.");
			}
		}

		//Get all friendProfiles by profile id
		public List<FriendProfile> GetAll(long profileId)
		{
			try
			{
				return _friendRepository.GetAll(profileId);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve all friends.");
			}
		}

		//Get all favourite friendProfiles by profile id
		public List<FriendProfile> GetAllFavourites(long profileId)
		{
			try
			{
				return _friendRepository.GetAllFavourites(profileId);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve all favourites friends.");
			}
		}

		//Add friendProfile
		public FriendProfile Add(FriendProfile friendProfile)
		{
			try
			{
				return _friendRepository.Add(friendProfile);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to add the user's friend.");
			}
		}

		//Update friendProfile
		public FriendProfile Update(FriendProfile friendProfile)
		{
			try
			{
				return _friendRepository.Update(friendProfile);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to update the user's friend.");
			}
		}
	}
}