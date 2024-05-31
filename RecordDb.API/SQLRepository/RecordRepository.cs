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

        public async Task<List<Record>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string?
            sortBy = null, bool isAscending = true)
        {
            var records = dbContext.Record.Include("Artist").AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                records = FilterRecords(records, filterOn, filterQuery);
            }
            else
            {
                records = RemoveTextFields(records);
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Recorded", StringComparison.OrdinalIgnoreCase))
                {
                    records = isAscending ? records.OrderBy(r => r.Recorded) : records.OrderByDescending(r => r.Recorded);
                }
            }

            return await records.ToListAsync();
        }

        private IQueryable<Record> FilterRecords(IQueryable<Record> records, string filterOn, string filterQuery)
        {
            if (filterOn.Equals("Field", StringComparison.OrdinalIgnoreCase))
            {
                records = records.Where(r => r.Field.Contains(filterQuery));
            }
            else if (filterOn.Equals("Media", StringComparison.OrdinalIgnoreCase))
            {
                records = records.Where(r => r.Media.Contains(filterQuery));
            }
            else if (filterOn.Equals("Review", StringComparison.OrdinalIgnoreCase))
            {
                string pattern = "%" + filterQuery + "%";
                records = records.Where(r => EF.Functions.Like(r.Review, pattern));
            }
            else if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                string pattern = "%" + filterQuery + "%";
                records = records.Where(r => EF.Functions.Like(r.Name, pattern));
            }
            else if (filterOn.Equals("ArtistName", StringComparison.OrdinalIgnoreCase))
            {
                string pattern = "%" + filterQuery + "%";
                records = records.Where(r => EF.Functions.Like(r.Artist.Name, pattern));
            }
            else if (filterOn.Equals("Recorded", StringComparison.OrdinalIgnoreCase))
            {
                records = records.Where(r => r.Recorded.Equals(int.Parse(filterQuery)));
            }

            records = RemoveTextFields(records);

            return records;
        }

        private IQueryable<Record> RemoveTextFields(IQueryable<Record> records)
        {
            foreach (Record record in records) 
            { 
                record.Review = null;
                record.Artist.Biography = null;
            }

            return records;
        }

        public async Task<Record?> GetByIdAsync(int id)
        {
            return await dbContext.Record.Include("Artist").FirstOrDefaultAsync(r => r.RecordId == id);
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
