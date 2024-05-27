using RecordDb.API.Models.Domain;

namespace RecordDb.API.SQLRepository
{
    public interface IRecordRepository
    {
        Task<List<Record>> GetAllAsync();

        Task<Record?> GetByIdAsync(int id);

        Task<Record> CreateAsync(Record record);

        Task<Record?> UpdateAsync(int id, Record record);

        Task<Record?> DeleteAsync(int id);
    }
}
