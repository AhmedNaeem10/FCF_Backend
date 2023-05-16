using FCF.Data;
using FCF.Entities;
using FCF.Models.Requests.TournamentDtos;
using FCF.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCF.Services.Implementations
{
    public class TournamentService : ITournamentService
    {
        private readonly MainDBContext dbContext;
        private readonly IEmailService emailService;
        public TournamentService(MainDBContext dbContext, IEmailService emailService)
        {
            this.dbContext = dbContext;
            this.emailService = emailService;
        }

        public async Task<Tournament> GetAsync(int id)
        {
            var tournament = await dbContext.Tournaments.FindAsync(id);
            return tournament;
        }

        public async Task<List<Tournament>> GetAllAsync()
        {
            var tournaments = await dbContext.Tournaments.ToListAsync();
            return tournaments;
        }

        public async Task<Tournament> CreateAsync(CreateTournamentDto tournament)
        {

            Tournament newTournament = new Tournament()
            {
                Name = tournament.Name,
                Description = tournament.Description,
                CreatedAt = DateTime.Now,
                ScheduledAt = tournament.Scheduled,
            };
            await dbContext.Tournaments.AddAsync(newTournament);
            await dbContext.SaveChangesAsync();
            return newTournament;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tournament = await dbContext.Tournaments.FindAsync(id);
            dbContext.Remove(tournament);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
