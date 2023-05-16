using FCF.Data;
using FCF.Entities;
using Microsoft.EntityFrameworkCore;
using FCF.Services.Interfaces;
using FCF.Models.Requests.MatchDtos;

namespace FCF.Services.Services
{

    public class MatchService : IMatchService
    {
        private readonly MainDBContext dbContext;
        public MatchService(MainDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Match> CreateMatchAsync(MatchDto match)
        {
            var match_ = new Match()
            {
                TeamId1 = match.TeamId1,
                TeamId2 = match.TeamId2,
                VenueId = match.VenueId,
                TournamentId = match.TournamentId,
                Date_time = DateTime.Now
            };
            await dbContext.Matches.AddAsync(match_);
            await dbContext.SaveChangesAsync();
            return match_;
        }

        public async Task<List<Match>> GetAllMatchesAsync()
        {
            return await dbContext.Matches.ToListAsync();
        }

        public async Task<Match> GetByIdAsync(int id)
        {
            return await dbContext.Matches.FindAsync(id);
        }
    }
}
