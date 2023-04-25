using FCF.Entities;

namespace FCF.Models.Requests.UserDtos;

public class UserInfo
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Division { get; set; }
    public string Designation { get; set; }
    public int? TeamId { get; set; }
    public string token { get; set; }

    public string Role { get; set; }
    public Team Team_ { get; set; }


    public UserInfo(User user, string token)
    {
        UserId = user.UserId;
        Name = user.Name;
        Email = user.Email;
        Password = user.Password;
        Division = user.Division;
        Designation = user.Designation;
        TeamId = user.TeamId;
        Team_ = user.Team_;
        Role = user.Role;
        this.token = token;
    }
}
