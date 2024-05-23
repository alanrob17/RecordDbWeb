using Microsoft.EntityFrameworkCore;
using RecordDb.API.Models.Domain;

namespace RecordDb.API.Data
{
    public class RecordDbContext: DbContext
    {
        public RecordDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Artist> Artist { get; set; }

        public DbSet<Record> Record { get; set; }
    }
}
