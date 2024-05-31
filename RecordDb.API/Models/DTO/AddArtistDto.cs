using System.ComponentModel.DataAnnotations;

namespace RecordDb.API.Models.DTO
{
    public class AddArtistDto
    {
        [MaxLength(50, ErrorMessage = "Artist FirstName can't be larger than 50 characters!")]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage ="Artist LastName can't be larger than 50 characters!")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage ="Artist LastName can't be larger than 50 characters!")]
        public string Name { get; set; }

        public string? Biography { get; set; }
    }
}
