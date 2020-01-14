using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Dating.Common.Models
{
    //Relates a profile to the user's hobbies.
    [Table("dat_profile_hobbies")]
    public class ProfileHobbie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("dat_pro_hob_id")]
        public long Id { get; set; }
     
        [Column("dat_pro_id")]
        [ForeignKey("Profile")]
        [Required]
        public long ProfileId { get; set; }

        [XmlIgnore]
        public virtual Profile Profile { get; set; }

        [Column("dat_hob_id")]
        [ForeignKey("Hobbie")]
        [Required]
        public long HobbieId { get; set; }
        public virtual Hobbie Hobbie { get; set; }
    }
}