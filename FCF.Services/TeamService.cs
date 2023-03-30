using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCF.Models;
using FCF.Data;
using FCF.Entities;
using Microsoft.EntityFrameworkCore;

namespace FCF.Services
{
    public interface ITeamService
    {
        public Task<Team> GetByIdAsync(int id);
        public Task<Team> AddTeamAsync(TeamDto team);
        public Task<List<Team>> GetAllTeamsAsync();
    }
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
            try
            {
                if (ValidateTeam(team))
                {
                    var newTeam = new Team()
                    {
                        Name = team.teamName
                    };
                    await dbContext.Teams.AddAsync(newTeam);
                    await dbContext.SaveChangesAsync();
                    return newTeam;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            return await dbContext.Teams.ToListAsync();
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            return await dbContext.Teams.SingleOrDefaultAsync(x => x.TeamId == id);
        }
    }
}
