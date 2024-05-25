using RecordDb.API.Models.Domain;

namespace RecordDb.API.SQLRepository
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetAllAsync();

        Task<Artist?> GetByIdAsync(int id);

        Task<Artist> CreateAsync(Artist artist);

        Task<Artist?> UpdateAsync(int id, Artist artist);

        Task<Artist?> DeleteAsync(int id);
    }
}
