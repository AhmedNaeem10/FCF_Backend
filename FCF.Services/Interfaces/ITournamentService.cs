using FCF.Entities;
using FCF.Models.Requests.TournamentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCF.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<List<Tournament>> GetAllAsync();
        Task<Tournament> GetAsync(int id);
        Task<Tournament> CreateAsync(CreateTournamentDto tournamentDto);
        Task<bool> DeleteAsync(int id);
    }
}
