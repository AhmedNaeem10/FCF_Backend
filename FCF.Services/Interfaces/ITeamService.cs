using FCF.Entities;
using FCF.Models.Requests.TeamDtos;

namespace FCF.Services.Interfaces
{
    public interface ITeamService
    {
        public Task<Team> GetByIdAsync(int id);
        public Task<Team> AddTeamAsync(TeamDto team);
        public Task<List<Team>> GetAllTeamsAsync();
        public Task<Team> RegisterTeamInTournament(int teamId, int tourId);
    }
}
