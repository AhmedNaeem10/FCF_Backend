using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FCF.Entities
{
    public class User

    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Division { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        public string Role { get; set; }

        public int? TeamId { get; set; }
        [ForeignKey("TeamId")]
        [AllowNull]
        public virtual Team? Team_ { get; set; }

    }
}
