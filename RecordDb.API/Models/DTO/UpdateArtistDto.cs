using System.ComponentModel.DataAnnotations;

namespace RecordDb.API.Models.DTO
{
    public class UpdateArtistDto
    {
        public string? FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Biography { get; set; }
    }
}
