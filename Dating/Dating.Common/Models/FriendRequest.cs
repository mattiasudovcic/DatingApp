using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dating.Common.Models
{
	//Relates two friends through a friend request.
	[Table("dat_friend_requests")]
	public class FriendRequest
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("dat_fri_req_id")]
		public long Id { get; set; }

		[Column("dat_fri_req_usr_id")]
		[Required]
		public string UserId { get; set; }


		[Column("dat_pro_id")]
		[ForeignKey("Profile")]
		[Required]
		public long ProfileId { get; set; }
		public virtual Profile Profile { get; set; }

		[Column("dat_fri_req_fri_usr_id")]
		[Required]
		public string FriendRequestUserId { get; set; }

		[Column("dat_fri_req_pro_id")]
		[ForeignKey("FriendRequestProfile")]
		[Required]
		public long FriendRequestProfileId { get; set; }
		public virtual Profile FriendRequestProfile { get; set; }



		[Column("dat_fri_req_is_friend")]
		[Required]
		public bool IsFriend { get; set; }



	}
}