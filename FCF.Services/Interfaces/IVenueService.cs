using FCF.Entities;
using FCF.Models.Requests.VenueDtos;

namespace FCF.Services.Interfaces
{
    public interface IVenueService
    {
        public Task<Venue> GetByIdAsync(int id);

        public Task<List<Venue>> GetAllAsync();

        public Task<Venue> AddAsync(VenueDto venue);

        public Task<bool> DeleteAsync(int id);

        public Task<Venue> UpdateAsync(int id, VenueDto updatedVenue);
    }
}
