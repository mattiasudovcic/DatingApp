using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dating.Common.Models
{
	[Table("dat_visitors_profile")]
	public class VisitorProfile
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("dat_vis_pro_id")]
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

		[Column("dat_vis_date")]
		[Required]
		public DateTime Date { get; set; }



	}
}