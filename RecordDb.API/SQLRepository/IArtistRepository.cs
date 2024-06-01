using RecordDb.API.Models.Domain;

namespace RecordDb.API.SQLRepository
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 15);

        Task<Artist?> GetByIdAsync(int id);

        Task<Artist> CreateAsync(Artist artist);

        Task<Artist?> UpdateAsync(int id, Artist artist);

        Task<Artist?> DeleteAsync(int id);
    }
}
