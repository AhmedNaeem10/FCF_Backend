using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCF.Entities
{
    public class Match
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date_time { get; set; }

        public int? TeamId1 { get; set; }

        [ForeignKey("TeamId1")]
        public Team? Team1 { get; set; }

        public int? TeamId2 { get; set; }

        [ForeignKey("TeamId2")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Team? Team2 { get; set; }

        public int? VenueId { get; set; }

        [ForeignKey("VenueId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Venue? Venue { get; set; }

        public int? TournamentId { get; set; }

        [ForeignKey("TournamentId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Tournament? Tournament { get; set; }

    }
}
