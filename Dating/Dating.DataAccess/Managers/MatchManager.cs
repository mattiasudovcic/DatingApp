using Dating.Common.Enums;
using Dating.Common.Models;
using Dating.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dating.DataAccess.Managers
{
	public class MatchManager
	{
		private readonly ProfileRepository _profileRepository;
		public MatchManager()
		{
			_profileRepository = new ProfileRepository();
		}

		//Get all potential matches by profile.
		public List<Profile> GetMatchesByProfile(Profile userProfile)
		{
			try
			{
				/*We bring only the profiles of the sexual orientation of the user. It is not necessary to bring all profiles.*/
				long? genderTarget = null;
				if (userProfile.SexualOrientationId == (long)SexualOrientationEnum.Man)
					genderTarget = (long)GenderEnum.Male;
				if (userProfile.SexualOrientationId == (long)SexualOrientationEnum.Woman)
					genderTarget = (long)GenderEnum.Female;
				var allProfiles = _profileRepository.GetByFilters(string.Empty, genderTarget);

				Func<Profile, bool> filter = delegate (Profile friendProfile)
				{

					return IsMatch(userProfile, friendProfile);

				};

				var query = allProfiles.Where(filter);

				return query.ToList();

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve user's match.");
			}
		}


		/*
		 * Receive two profiles and determine if matches are possible.
		   They must meet the following requirements:
	       They must be two different profiles.
		   The age of each user must be the target age of the other user.
           Users must agree on sexual orientation.
           Users must match at least two hobbies.
		 */
		public bool IsMatch(Profile userProfile, Profile friendProfile)
		{
			var userAge = GetAge(userProfile.BirthDate);
			var friendAge = GetAge(friendProfile.BirthDate);

			bool distinctProfile = userProfile.Id != friendProfile.Id;

			bool ageMatch = userProfile.MinTargetAge <= friendAge && userProfile.MaxTargetAge >= friendAge &&
							friendProfile.MinTargetAge <= userAge && friendProfile.MaxTargetAge >= userAge;

			if (!ageMatch) return false;

			long? friendGenderTarget = null;
			if (friendProfile.SexualOrientationId == (long)SexualOrientationEnum.Man)
				friendGenderTarget = (long)GenderEnum.Male;
			if (friendProfile.SexualOrientationId == (long)SexualOrientationEnum.Woman)
				friendGenderTarget = (long)GenderEnum.Female;

			bool sexualOrientationMatch = userProfile.GenderId == friendGenderTarget;

			if (!sexualOrientationMatch) return false;


			const int minHobbiesMatch = 3;
			bool hobbieMatch = userProfile.Hobbies.Select(x => x.HobbieId).Intersect(friendProfile.Hobbies.Select(x => x.HobbieId)).Count() >= minHobbiesMatch;

			return distinctProfile && ageMatch && sexualOrientationMatch && hobbieMatch;
		}


		//Get the user's age from the date of birth
		private int GetAge(DateTime birthDate)
		{
			// Save today's date.
			var today = DateTime.Today;
			// Calculate the age.
			var age = today.Year - birthDate.Year;
			// Go back to the year the person was born in case of a leap year
			if (birthDate.Date > today.AddYears(-age)) age--;

			return age;
		}
	}
}