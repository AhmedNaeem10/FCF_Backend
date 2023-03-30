﻿using FCF.Models;
using FCF.Data;
using FCF.Entities;
using Microsoft.EntityFrameworkCore;

namespace FCF.Services
{
    public interface IMatchService
    {
        public Task<Match> CreateMatchAsync(MatchDto match);

        public Task<List<Match>> GetAllMatchesAsync();

        public Task<Match> GetByIdAsync(int id);
    }
    public class MatchService : IMatchService
    {
        private readonly MainDBContext dbContext;
        public MatchService(MainDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Match> CreateMatchAsync(MatchDto match)
        {
            try
            {
                var match_ = new Match()
                {
                    TeamId1 = match.TeamId1,
                    TeamId2 = match.TeamId2,
                    VenueId = match.VenueId,
                    Date_time = new DateTime()
                };
                await dbContext.Matches.AddAsync(match_);
                await dbContext.SaveChangesAsync();
                return match_;
            }
            catch (Exception ex)
            {
                return null;
            }
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
