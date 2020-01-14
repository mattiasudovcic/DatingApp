using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dating.Helpers
{
	public class ActionHelper
	{
		public const string GET_JSON_LIST_CHECK_BOX_HOBBIES = "GetJsonListCheckBoxHobbies";
		public const string CREATE_PROFILE = "Create";
		public const string EDIT_PROFILE = "Edit";
		public const string MY_PROFILE = "MyProfile";
		public const string VIEW_PROFILE = "ViewProfile";
		public const string VALIDATE_PROFILE = "ValidateProfile";
		public const string LOGIN = "Login";
		public const string USER_SEARCH = "Profiles";
		public const string ADD_FRIEND = "/api/FriendRequests/FriendRequest/add?friendsId=";
		public const string ACCEPT_FRIEND = "/api/FriendRequests/FriendRequest/accept?friendRequestId=";
		public const string DECLINE_FRIEND = "/api/FriendRequests/FriendRequest/decline?friendRequestId=";
		public const string LAST_VISITORS = "/api/Visitors/Visitor/getLast";
		public const string FRIEND_REQUESTS = "FriendRequests";
		public const string IS_MATCH = "IsMatch";
		public const string ADD_FAVOURITE = "AddFavourite";
		public const string REMOVE_FAVOURITE = "RemoveFavourite";
	}
}