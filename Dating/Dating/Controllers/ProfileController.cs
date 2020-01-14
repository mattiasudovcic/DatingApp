using Dating.Common.Enums;
using Dating.Common.Models;
using Dating.Common.Validators;
using Dating.DataAccess.Managers;
using Dating.Helpers;
using Dating.Models;
using FluentValidation.Results;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dating.Controllers
{
	[Authorize]
	public class ProfileController : Controller
	{
		private readonly ProfileManager _profileManager;
		private readonly HobbieManager _hobbieManager;
		private readonly FriendRequestManager _friendRequestManager;
		private readonly FriendProfileManager _friendProfileManager;
		private readonly VisitorProfileManager _visitorProfileManager;
		private readonly ProfilePostManager _profilePostManager;

		public ProfileController()
		{
			_profileManager = new ProfileManager();
			_hobbieManager = new HobbieManager();
			_friendRequestManager = new FriendRequestManager();
			_friendProfileManager = new FriendProfileManager();
			_visitorProfileManager = new VisitorProfileManager();
			_profilePostManager = new ProfilePostManager();
		}

		//View all profiles.  Search by username or first name or last name. 
		//Only profiles that match the user's sexual orientation.
		[CustomerAuthorize]
		public ActionResult Profiles(string searchString)
		{
			try
			{
				var profile = _profileManager.GetByUserId(User.Identity.GetUserId());

				var profiles = _profileManager.GetByFilters(searchString, profile.SexualOrientationId);

				return View(profiles.ToList());
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "ErrorHandler", new { @message = e.Message });
			}
		}


		//View my profile
		[CustomerAuthorize]
		public ActionResult ViewProfile(long profileId)
		{
			try
			{

				var currentProfile = _profileManager.GetByUserId(User.Identity.GetUserId());
				if (profileId == currentProfile.Id)
					return RedirectToAction("MyProfile");

				var profileViewModel = new ProfileViewModel();

				profileViewModel.Profile = _profileManager.GetById(profileId); //Get current profile 
				profileViewModel.Friends = _friendProfileManager.GetAll(profileViewModel.Profile.Id); //Get all friends by profile

				ViewBag.ProfileId = profileId;
				ViewBag.FriendProfileId = currentProfile.Id;
				ViewBag.IsFriend = _friendRequestManager.AreFriends(User.Identity.GetUserId(), profileViewModel.Profile.UserId); //Two profiles are friend.
				ViewBag.PendingFriend = _friendRequestManager.Exists(User.Identity.GetUserId(), profileViewModel.Profile.UserId); //is a pending Friend request 

				profileViewModel.Friends = _friendProfileManager.GetAllFavourites(profileViewModel.Profile.Id); //Get all favourite friends by profile.
				profileViewModel.Posts = _profilePostManager.GetAll(profileViewModel.Profile.Id); //Get all post by profile

				if (profileId != currentProfile.Id) //If logged users is diferrent a user's profile then add a visitor as visitant
				{
					_visitorProfileManager.Add(new VisitorProfile() //If logged user is diferrent a visited user's profile then add a logged user as visitor
					{
						ProfileId = profileId,
						FriendProfileId = currentProfile.Id,
						Date = DateTime.Now
					});
				}
				return View("Profile", profileViewModel);
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "ErrorHandler", new { @message = e.Message });
			}
		}

		// MyProfile
		[CustomerAuthorize]
		public ActionResult MyProfile()
		{
			try
			{
				var profileViewModel = new ProfileViewModel();
				var currentProfile = _profileManager.GetByUserId(User.Identity.GetUserId());
				ViewBag.ProfileId = currentProfile.Id;
				ViewBag.FriendProfileId = currentProfile.Id;
				profileViewModel.Profile = _profileManager.GetByUserId(User.Identity.GetUserId());
				profileViewModel.Friends = _friendProfileManager.GetAllFavourites(profileViewModel.Profile.Id);
				profileViewModel.Posts = _profilePostManager.GetAll(profileViewModel.Profile.Id);
				return View("Profile", profileViewModel);
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "ErrorHandler", new { @message = e.Message });
			}
		}

	
		[CustomerAuthorize]
		public ActionResult DisableAccount()
		{
			try
			{
				var profileViewModel = new ProfileViewModel();
				profileViewModel.Profile = _profileManager.GetByUserId(User.Identity.GetUserId());
				profileViewModel.Profile.Active = false;
				_profileManager.Update(profileViewModel.Profile);
				return View("Profile", profileViewModel);
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "ErrorHandler", new { @message = e.Message });
			}
		}

		[CustomerAuthorize]
		public ActionResult ActiveAccount()
		{
			try
			{
				var profileViewModel = new ProfileViewModel();
				profileViewModel.Profile = _profileManager.GetByUserId(User.Identity.GetUserId());
				profileViewModel.Profile.Active = true;
				_profileManager.Update(profileViewModel.Profile);
				return View("Profile", profileViewModel);
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "ErrorHandler", new { @message = e.Message });
			}
		}

		
		public ActionResult Create()
		{
			try
			{
				var profile = new Profile();
				var listHobbies = new List<long>();
				foreach (var hobbie in _hobbieManager.GetAll())
				{
					listHobbies.Add(hobbie.Id);
				}

				ViewBag.Hobbies = listHobbies;
				var genders = from GenderEnum g in Enum.GetValues(typeof(GenderEnum))
							  select new EnumValues { ID = (int)g, Name = g.ToString() };
				ViewBag.Genders = new SelectList(genders, "ID", "Name");

				var sexualOrientations = from SexualOrientationEnum so in Enum.GetValues(typeof(SexualOrientationEnum))
										 select new EnumValues { ID = (int)so, Name = so.ToString() };
				ViewBag.SexualOrientations = new SelectList(sexualOrientations, "ID", "Name");

				return View("Create", profile);
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "ErrorHandler", new { @message = e.Message });
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Profile model)
		{
			var userId = User.Identity.GetUserId();

			var newProfile = new Profile
			{
				UserId = userId,
				UserName = model.UserName,
				FirstName = model.FirstName,
				LastName = model.LastName,
				BirthDate = model.BirthDate,
				CreatedDate = DateTime.Today,
				Email = User.Identity.GetUserName(),
				GenderId = model.GenderId,
				SexualOrientationId = model.SexualOrientationId,
				MinTargetAge = model.MinTargetAge,
				MaxTargetAge = model.MaxTargetAge,
				Hobbies = model.Hobbies,
				Active = true

			};		

			var listHobbies = new List<long>();
			foreach (var hobbie in _hobbieManager.GetAll())
			{
				listHobbies.Add(hobbie.Id);
			}
			ViewBag.Hobbies = listHobbies;

			var genders = from GenderEnum g in Enum.GetValues(typeof(GenderEnum))
						  select new EnumValues { ID = (int)g, Name = g.ToString() };
			ViewBag.Genders = new SelectList(genders, "ID", "Name");

			var sexualOrientations = from SexualOrientationEnum so in Enum.GetValues(typeof(SexualOrientationEnum))
									 select new EnumValues { ID = (int)so, Name = so.ToString() };
			ViewBag.SexualOrientations = new SelectList(sexualOrientations, "ID", "Name");


			if (ModelState.IsValid)
			{
				_profileManager.Add(newProfile);

				return RedirectToAction("MyProfile", "Profile");
			}

			return View("Create", model);

		}

		[CustomerAuthorize]
		public ActionResult Edit()
		{
			try
			{
				var profile = _profileManager.GetByUserId(User.Identity.GetUserId());
				var listHobbies = new List<ProfileHobbieViewModel>();
				foreach (var profileHobbie in profile.Hobbies)
				{
					listHobbies.Add(new ProfileHobbieViewModel()
					{
						Id = profileHobbie.Id,
						ProfileId = profileHobbie.ProfileId,
						HobbieId = profileHobbie.HobbieId
					});
				}
				ViewBag.Hobbies = listHobbies;

				var genders = from GenderEnum g in Enum.GetValues(typeof(GenderEnum))
							  select new EnumValues { ID = (int)g, Name = g.ToString() };
				ViewBag.Genders = new SelectList(genders, "ID", "Name");

				var sexualOrientations = from SexualOrientationEnum so in Enum.GetValues(typeof(SexualOrientationEnum))
										 select new EnumValues { ID = (int)so, Name = so.ToString() };
				ViewBag.SexualOrientations = new SelectList(sexualOrientations, "ID", "Name");

				return View("Edit", profile);
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "ErrorHandler", new { @message = e.Message });
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Profile model)
		{
			var userId = User.Identity.GetUserId();
			var currentProfile = _profileManager.GetByUserId(userId);


			currentProfile.UserName = model.UserName;
			currentProfile.FirstName = model.FirstName;
			currentProfile.LastName = model.LastName;
			currentProfile.BirthDate = model.BirthDate;
			currentProfile.Email = User.Identity.GetUserName();
			currentProfile.GenderId = model.GenderId;
			currentProfile.SexualOrientationId = model.SexualOrientationId;
			currentProfile.MinTargetAge = model.MinTargetAge;
			currentProfile.MaxTargetAge = model.MaxTargetAge;
			currentProfile.Hobbies = model.Hobbies;

			if (ModelState.IsValid)
			{
				_profileManager.Update(currentProfile);

				return RedirectToAction("MyProfile", "Profile");
			}

			var listHobbies = new List<ProfileHobbieViewModel>();
			if (model.Hobbies != null)
			{
				foreach (var profileHobbie in model.Hobbies)
				{
					listHobbies.Add(new ProfileHobbieViewModel()
					{
						Id = profileHobbie.Id,
						ProfileId = profileHobbie.ProfileId,
						HobbieId = profileHobbie.HobbieId
					});
				}
			}
			ViewBag.Hobbies = listHobbies;

			var genders = from GenderEnum g in Enum.GetValues(typeof(GenderEnum))
						  select new EnumValues { ID = (int)g, Name = g.ToString() };
			ViewBag.Genders = new SelectList(genders, "ID", "Name");

			var sexualOrientations = from SexualOrientationEnum so in Enum.GetValues(typeof(SexualOrientationEnum))
									 select new EnumValues { ID = (int)so, Name = so.ToString() };
			ViewBag.SexualOrientations = new SelectList(sexualOrientations, "ID", "Name");

			return View("Edit", model);
		}


		//Returns if a user's profile is valid. 
		//Validate that the user's name is unique.
		//Validates that the user has at least two hobbies.
		public JsonResult ValidateProfile(Profile profile)
		{
			var currentProfile = _profileManager.GetByUserId(User.Identity.GetUserId());

			ProfileValidator validator = new ProfileValidator();

			ValidationResult validationResults = validator.Validate(profile);
			List<string> errorMessages = new List<string>();

			// Add the errors from our validation into the error messages variable and return it.
			if (!validationResults.IsValid)
			{
				foreach (var error in validationResults.Errors)
				{
					if (currentProfile != null && error.PropertyName == "UserName") continue;
					errorMessages.Add(error.ErrorMessage);
				
				}
			}

			return Json(errorMessages, JsonRequestBehavior.AllowGet);
		}

		[Authorize]
		[HttpPost]
		public ActionResult UpdateProfilePicture(HttpPostedFileBase imageFile)
		{
			try
			{
				string userId = User.Identity.GetUserId();

				var currentProfile = _profileManager.GetByUserId(userId);

				//If the user already has a profile picture, we delete it.
				if (currentProfile.ImagePath != null)
				{
					string fullPath = Request.MapPath("~" + currentProfile.ImagePath);
					if (System.IO.File.Exists(fullPath))
					{
						System.IO.File.Delete(fullPath);
						currentProfile.ImagePath = null;
						_profileManager.Update(currentProfile);
					}
				}

				string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
				string extension = Path.GetExtension(imageFile.FileName);
				fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
				fileName = fileName.Replace(" ", "_");
				currentProfile.ImagePath = "~/Images/" + fileName;
				string imagePath = "/Images/" + fileName;

				//Save image into images directory
				fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
				imageFile.SaveAs(fileName);

				currentProfile.ImagePath = imagePath;

				//Update profile picture
				_profileManager.Update(currentProfile);


			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "ErrorHandler", new { @message = e.Message });
			}
			return RedirectToAction("MyProfile", "Profile");

		}

		//View all pending friend request
		[CustomerAuthorize]
		public ActionResult FriendRequests()
		{

			var userId = User.Identity.GetUserId();
			var friendsList = _friendRequestManager.GetFriendRequests(userId);
			return View("FriendRequests", friendsList);
		}

		//Count pending friend requests
		public ActionResult CountFriendRequests()
		{

			var userId = User.Identity.GetUserId();
			var currentProfile = _profileManager.GetByUserId(userId);
			if (currentProfile == null)
			{
				return Content("0");
			}
			var friendsList = _friendRequestManager.GetFriendRequests(userId);

			return Content(friendsList.Count.ToString());
		}



	}
}