using System.ComponentModel.DataAnnotations;

namespace RecordDb.API.Models.DTO
{
    public class AddRecordDto
    {
        [Required]
        public int ArtistId { get; set; } // relate to the artist entity

        [Required]
        [MaxLength(80, ErrorMessage = "Record Name can't be larger than 80 characters!")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Field can't be larger than 50 characters!")]
        public string Field { get; set; }

        [Required]
        [Range(1900, 2024, ErrorMessage = "Recorded must be between 1900 and 2024!")]
        public int Recorded { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Label can't be larger than 50 characters!")]
        public string Label { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Pressing can't be larger than 50 characters!")]
        public string Pressing { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Rating can't be less than 1 character!")]
        [MaxLength(4, ErrorMessage = "Rating can't be larger than 4 characters!")]
        public string Rating { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Discs must be between 1 and 10!")]
        public int Discs { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Media can't be larger than 50 characters!")]
        public string Media { get; set; }

        public DateTime? Bought { get; set; }

        public decimal? Cost { get; set; }

        public string? CoverName { get; set; }

        public string? Review { get; set; }
    }
}
