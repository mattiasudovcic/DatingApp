using Dating.Common.Models;
using Dating.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dating.DataAccess.Managers
{
	public class VisitorProfileManager
	{
		private readonly VisitorProfileRepository _visitorProfileRepository;

		public VisitorProfileManager()
		{
			_visitorProfileRepository = new VisitorProfileRepository();
		}

		//Get last five visitors.
		//Used from user's profile
		public List<Profile> GetLastFiveVisitors(long profileId)
		{
			try
			{
				return _visitorProfileRepository.GetLastFiveVisitors(profileId);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve all visitors.");
			}
		}

		//Add visitorProfile
		public VisitorProfile Add(VisitorProfile visitorProfile)
		{
			try
			{
				return _visitorProfileRepository.Add(visitorProfile);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to add a visitor profile");
			}
		}

	}
}