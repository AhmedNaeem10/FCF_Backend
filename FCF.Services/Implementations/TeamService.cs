using FCF.Data;
using FCF.Entities;
using Microsoft.EntityFrameworkCore;
using FCF.Services.Interfaces;
using FCF.Models.Requests.TeamDtos;

namespace FCF.Services.Services
{
    public class TeamService : ITeamService
    {
        private readonly MainDBContext dbContext;

        public TeamService(MainDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private bool ValidateTeam(TeamDto team)
        {
            // Will apply proper validation, checking empty strings for now
            if (team.teamName.Trim() == "")
            {
                return false;
            }
            return true;
        }
        public async Task<Team> AddTeamAsync(TeamDto team)
        {
            var newTeam = new Team()
            {
                Name = team.teamName
            };
            await dbContext.Teams.AddAsync(newTeam);
            await dbContext.SaveChangesAsync();
            return newTeam;
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            return await dbContext.Teams.ToListAsync();
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            return await dbContext.Teams.SingleOrDefaultAsync(x => x.TeamId == id);
        }

        public async Task<Team> RegisterTeamInTournament(int teamId, int tourId)
        {
            var team = await dbContext.Teams.SingleOrDefaultAsync(x => x.TeamId == teamId);
            team.TournamentId = tourId;
            await dbContext.SaveChangesAsync();
            return team;
        }
    }
}
