using Dating.DataAccess.Managers;
using Dating.Helpers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dating.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly ProfileManager _profileManager;
        private readonly FriendProfileManager _friendProfileManager;

        public FriendController()
        {
            _profileManager = new ProfileManager();
            _friendProfileManager = new FriendProfileManager();
        }

        [CustomerAuthorize]
        public ActionResult Index()
        {
            var currentProfile = _profileManager.GetByUserId(User.Identity.GetUserId());
            var friends = _friendProfileManager.GetAll(currentProfile.Id);
            return View(friends);
        }

        //Add a friend as a favorite
        [CustomerAuthorize]
        public JsonResult AddFavourite(long friendProfileId)
        {
            try
            {
                var friendProfile = _friendProfileManager.GetById(friendProfileId);
                friendProfile.IsFavourite = true;
                var result = _friendProfileManager.Update(friendProfile);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        //Remove a friend as a favorite
        [CustomerAuthorize]
        public JsonResult RemoveFavourite(long friendProfileId)
        {
            try
            {
                var friendProfile = _friendProfileManager.GetById(friendProfileId);
                friendProfile.IsFavourite = false;
                var result = _friendProfileManager.Update(friendProfile);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}