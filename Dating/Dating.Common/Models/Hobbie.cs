using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dating.Common.Models
{
    [Table("dat_hobbies")]
    public class Hobbie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("dat_hob_id")]
        public long Id { get; set; }

        [Column("dat_hob_name")]
        public string Name { get; set; }

    }
}