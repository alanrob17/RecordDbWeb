using Microsoft.EntityFrameworkCore;
using RecordDb.API.Data;
using RecordDb.API.Models.Domain;

namespace RecordDb.API.SQLRepository
{
    public class RecordRepository : IRecordRepository
    {
        private readonly RecordDbContext dbContext;

        public RecordRepository(RecordDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<Record> CreateAsync(Record record)
        {
            await dbContext.Record.AddAsync(record);
            await dbContext.SaveChangesAsync();

            return record;
        }

        public async Task<List<Record>> GetAllAsync()
        {
            return await dbContext.Record.ToListAsync();
        }

        public async Task<Record?> GetByIdAsync(int id)
        {
            return await dbContext.Record.FirstOrDefaultAsync(r => r.RecordId == id);
        }

        public async Task<Record?> UpdateAsync(int id, Record record)
        {
            var existingRecord = await dbContext.Record.FirstOrDefaultAsync(b => b.RecordId == id);

            if (existingRecord == null)
            {
                return null;
            }

            existingRecord.ArtistId = record.ArtistId;
            existingRecord.Name = record.Name;
            existingRecord.Field = record.Field;
            existingRecord.Recorded = record.Recorded;
            existingRecord.Label = record.Label;
            existingRecord.Pressing = record.Pressing;
            existingRecord.Rating = record.Rating;
            existingRecord.Discs = record.Discs;
            existingRecord.Media = record.Media;
            existingRecord.Bought = record.Bought;
            existingRecord.Cost = record.Cost;
            existingRecord.CoverName = record.CoverName;
            existingRecord.Review = record.Review;

            await dbContext.SaveChangesAsync();

            return existingRecord;
        }

        public async Task<Record?> DeleteAsync(int id)
        {
            var record = await dbContext.Record.FindAsync(id);
            if (record == null)
            {
                return null;
            }

            dbContext.Record.Remove(record);
            await dbContext.SaveChangesAsync();

            return record;
        }
    }
}
