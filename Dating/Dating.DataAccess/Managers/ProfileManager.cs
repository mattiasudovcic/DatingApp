using Dating.Common.Enums;
using Dating.Common.Models;
using Dating.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dating.DataAccess.Managers
{
	public class ProfileManager
	{
		private readonly ProfileRepository _profileRepository;

		public ProfileManager()
		{
			_profileRepository = new ProfileRepository();
		}

		//Get all profiles
		public List<Profile> GetAll()
		{
			try
			{

				return _profileRepository.GetAll();

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve user's profile.");
			}
		}

		//Get all profiles by filters
		//Used from the user search.
		public List<Profile> GetByFilters(string searchString, long sexualOrientation)
		{
			try
			{
				long? genderTarget = null;
				if (sexualOrientation == (long)SexualOrientationEnum.Man)
					genderTarget = (long)GenderEnum.Male;
				if (sexualOrientation == (long)SexualOrientationEnum.Woman)
					genderTarget = (long)GenderEnum.Female;
				return _profileRepository.GetByFilters(searchString, genderTarget);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve user's profile.");
			}
		}


		//Get profile by user id
		public Profile GetByUserId(string userId)
		{
			try
			{
				return _profileRepository.GetByUserId(userId);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve the user's profile.");
			}
		}

		//Get profile by username
		public Profile GetByUserName(string userName)
		{
			try
			{
				return _profileRepository.GetByUserName(userName);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve the user's profile.");
			}
		}

		//Get profile by id
		public Profile GetById(long profileId)
		{
			try
			{
				return _profileRepository.GetById(profileId);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve the user's profile.");
			}
		}

		//Get last five profiles.
		//Used from home page. Recent logins.
		public List<Profile> GetFirstFiveProfiles()
		{
			try
			{
				return _profileRepository.GetFirstFiveProfiles();

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve the users's profile.");
			}
		}
				
		//Add profile
		public Profile Add(Profile profile)
		{
			try
			{
				return _profileRepository.Add(profile);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to add the user's profile.");
			}
		}

		//Update profile
		public Profile Update(Profile profile)
		{
			try
			{
				return _profileRepository.Update(profile);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to update the user's profile.");
			}
		}
	}
}