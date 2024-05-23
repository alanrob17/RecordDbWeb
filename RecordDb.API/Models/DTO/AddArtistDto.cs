using System.ComponentModel.DataAnnotations;

namespace RecordDb.API.Models.DTO
{
    public class AddArtistDto
    {
        public string? FirstName { get; set; }

        [Required]
        public string LastName { get; set; } // not null

        [Required]
        public string Name { get; set; }

        public string? Biography { get; set; }
    }
}
