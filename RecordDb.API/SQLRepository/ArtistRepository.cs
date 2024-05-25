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

        public async Task<List<Artist>> GetAllAsync()
        {
            return await dbContext.Artist.ToListAsync();
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
