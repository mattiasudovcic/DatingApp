using Dating.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace Dating.Common.Models
{  
    [Table("dat_profiles")]
    public class Profile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("dat_pro_id")]
        public long Id { get; set; }

        [Column("dat_usr_id")]  
        
        public string UserId { get; set; }


        [Column("dat_pro_active")]
        [Required] 
        public bool Active { get; set; }

        [Column("dat_pro_user_name")]
        [Required]
        [DisplayName("Username")]
        [StringLength(50)]
        [MinLength(3)]
        public string UserName { get; set; }
        
        [Column("dat_pro_first_name")]
        [Required]
        [DisplayName("First name")]
        [StringLength(50)]
        [MinLength(3)]
        public string FirstName { get; set; }
        
        [Column("dat_pro_last_name")]
        [Required]
        [DisplayName("Last name")]
        [StringLength(50)]
        [MinLength(3)]
        public string LastName { get; set; }

        [Column("dat_pro_email")]           
        public string Email { get; set; }

        [Column("dat_gender_id")]
        [Required]
        [DisplayName("Gender")]
        public long GenderId { get; set; }

        [NotMapped]
        public GenderEnum Gender { get; set; }

        [Column("dat_sexual_orientation_id")]
        [Required]
        [DisplayName("Sexual orientation")]
        public long SexualOrientationId { get; set; }

        [NotMapped]
        public SexualOrientationEnum SexualOrientation { get; set; }

        [Column("dat_min_target_age_id")]
        [Required]
        [DisplayName("Minium target age")]
        [Range(18,100)]
        public int MinTargetAge { get; set; }

        [Column("dat_max_target_age_id")]
        [Required]
        [DisplayName("Maxium target age")]
        [Range(18, 100)]
        public int MaxTargetAge { get; set; }

        [Column("dat_pro_birth_date")]
        [Required]
        [DisplayName("Birth date")]
        public DateTime BirthDate { get; set; }

        [Column("dat_pro_created_date")]
        [Required]
        [DisplayName("Date created")]
        public DateTime CreatedDate { get; set; }

        [Column("dat_pro_image_path")]           
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public virtual List<ProfileHobbie> Hobbies { get; set; }
        public virtual List<FriendProfile> FriendProfiles { get; set; }
        public virtual List<ProfilePost> Posts { get; set; }

     
    }
}