using Microsoft.EntityFrameworkCore;
using RecordDb.API.Data;
using RecordDb.API.Models.Domain;
using System.Reflection.Metadata;

namespace RecordDb.API.SQLRepository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly RecordDbContext dbContext;

        public ArtistRepository(RecordDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Artist> CreateAsync(Artist artist)
        {
            await dbContext.Artist.AddAsync(artist);
            await dbContext.SaveChangesAsync();

            return artist;
        }

        public async Task<Artist?> DeleteAsync(int id)
        {
            var artist = await dbContext.Artist.FindAsync(id);
            if (artist == null)
            {
                return null;
            }

            dbContext.Artist.Remove(artist);
            await dbContext.SaveChangesAsync();

            return artist;
        }

        public async Task<List<Artist>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 15)
        {
            var artists = dbContext.Artist.AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    string pattern = "%" + filterQuery + "%";
                    artists = artists.Where(r => EF.Functions.Like(r.Name, pattern));
                }

            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    artists = isAscending ? artists.OrderBy(r => r.LastName).ThenBy(r => r.FirstName) : artists.OrderByDescending(r => r.LastName).ThenBy(r => r.FirstName);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await artists.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Artist?> GetByIdAsync(int id)
        {
           return await dbContext.Artist.FirstOrDefaultAsync(a => a.ArtistId == id);
        }

        public async Task<Artist?> UpdateAsync(int id, Artist artist)
        {
            var existingArtist = await dbContext.Artist.FirstOrDefaultAsync(b => b.ArtistId == id);

            if (existingArtist == null)
            {
                return null;
            }

            existingArtist.FirstName = artist.FirstName;
            existingArtist.LastName = artist.LastName;
            existingArtist.Name = artist.Name;
            existingArtist.Biography = artist.Biography;

            await dbContext.SaveChangesAsync();

            return existingArtist;
        }
    }
}
