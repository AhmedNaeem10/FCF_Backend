using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FCF.Entities
{
    public class Venue
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VenueId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }
    }
}
