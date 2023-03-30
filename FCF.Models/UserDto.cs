using System.ComponentModel.DataAnnotations;

namespace FCF.Models
{
    public class userDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Division { get; set; }

        public string Designation { get; set; }

        public string Role { get; set; }
    }
}
