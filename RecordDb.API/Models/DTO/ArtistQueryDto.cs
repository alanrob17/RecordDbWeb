using RecordDb.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace RecordDb.API.Models.DTO
{
    public class ArtistQueryDto
    {
        [Required]
        public int ArtistId { get; set; } // identity field

        public string? FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
