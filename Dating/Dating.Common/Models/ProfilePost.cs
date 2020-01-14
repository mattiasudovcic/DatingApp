using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dating.Common.Models
{
	//Relates the profile with the posts on the wall.
	[Table("dat_profile_posts")]
	public class ProfilePost
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("dat_pro_pos_id")]
		public long Id { get; set; }

		[Column("dat_pro_id")]
		[ForeignKey("Profile")]
		[Required]
		public long ProfileId { get; set; }
		public virtual Profile Profile { get; set; }

		[Column("dat_fri_usr_pro_id")]
		[ForeignKey("FriendUserProfile")]
		[Required]
		public long FriendProfileId { get; set; }
		public virtual Profile FriendUserProfile { get; set; }

		[Column("dat_pro_pos_date")]
		[Required]
		public DateTime Date { get; set; }

		[Column("dat_pro_pos_message")]
		[Required]
		public string Message { get; set; }


	}
}