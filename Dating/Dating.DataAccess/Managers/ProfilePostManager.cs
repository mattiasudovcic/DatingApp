using Dating.Common.Models;
using Dating.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dating.DataAccess.Managers
{
	public class ProfilePostManager
	{
		private readonly ProfilePostRepository _friendRepository;

		public ProfilePostManager()
		{
			_friendRepository = new ProfilePostRepository();
		}

		//Get all profile post by profile id
		public List<ProfilePost> GetAll(long profileId)
		{
			try
			{
				return _friendRepository.GetAll(profileId);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve all posts.");
			}
		}

		//Add post
		public ProfilePost Add(ProfilePost profilePost)
		{
			try
			{
				return _friendRepository.Add(profilePost);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to add a post.");
			}
		}

		//update post
		public ProfilePost Update(ProfilePost profilePost)
		{
			try
			{
				return _friendRepository.Update(profilePost);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to update a post.");
			}
		}

		//Remove post
		public void Remove(long profilePostId)
		{
			try
			{
				_friendRepository.Remove(profilePostId);

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to remove a post");
			}
		}
	}
}