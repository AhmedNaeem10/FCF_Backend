using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCF.Entities
{
    public class Team
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }

        [Required]
        public string Name { get; set; }

        public int? TournamentId { get; set; }

        [ForeignKey("TournamentId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Tournament? Tournament { get; set; }

    }
}
