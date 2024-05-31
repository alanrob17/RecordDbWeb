using Microsoft.AspNetCore.Mvc;
using RecordDb.API.Models.Domain;

namespace RecordDb.API.SQLRepository
{
    public interface IRecordRepository
    {
        Task<List<Record>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);

        Task<Record?> GetByIdAsync(int id);

        Task<Record> CreateAsync(Record record);

        Task<Record?> UpdateAsync(int id, Record record);

        Task<Record?> DeleteAsync(int id);
    }
}
