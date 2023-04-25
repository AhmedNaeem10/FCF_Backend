using FCF.Entities;
using FCF.Models.Requests.MatchDtos;

namespace FCF.Services.Interfaces
{
    public interface IMatchService
    {
        public Task<Match> CreateMatchAsync(MatchDto match);

        public Task<List<Match>> GetAllMatchesAsync();

        public Task<Match> GetByIdAsync(int id);
    }
}
