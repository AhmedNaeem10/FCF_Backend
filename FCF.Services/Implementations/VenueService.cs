using FCF.Data;
using FCF.Entities;
using Microsoft.EntityFrameworkCore;
using FCF.Services.Interfaces;
using FCF.Models.Requests.VenueDtos;

namespace FCF.Services.Services
{

    public class VenueService : IVenueService
    {
        private readonly MainDBContext dbContext;

        public VenueService(MainDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Venue> GetByIdAsync(int id)
        {
            return await dbContext.Venues.SingleOrDefaultAsync(x => x.VenueId == id);
        }

        public async Task<List<Venue>> GetAllAsync()
        {
            return await dbContext.Venues.ToListAsync();
        }

        public async Task<Venue> AddAsync(VenueDto newVenue)
        {
            try
            {
                var venue = new Venue()
                {
                    Name = newVenue.VenueName,
                    Address = newVenue.Address,
                    City = newVenue.City
                };
                await dbContext.Venues.AddAsync(venue);
                await dbContext.SaveChangesAsync();
                return venue;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var venue = await dbContext.Venues.SingleOrDefaultAsync(x => x.VenueId == id);
                if (venue == null)
                {
                    return false;
                }
                dbContext.Venues.Remove(venue);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Venue> UpdateAsync(int id, VenueDto updatedVenue)
        {
            try
            {
                var venue = await dbContext.Venues.SingleOrDefaultAsync(x => x.VenueId == id);
                if (venue == null)
                {
                    return null;
                }
                venue.Name = updatedVenue.VenueName;
                await dbContext.SaveChangesAsync();
                return venue;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
