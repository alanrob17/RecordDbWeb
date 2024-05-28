using System.ComponentModel.DataAnnotations;

namespace RecordDb.API.Models.Domain
{
    public class Record
    {
        #region " Properties "

        [Required]
        public int RecordId { get; set; } // identity field

        [Required]
        public int ArtistId { get; set; } // relate to the artist entity

        [Required]
        public string Name { get; set; }

        [Required]
        public string Field { get; set; }

        [Required]
        public int Recorded { get; set; }

        [Required]
        public string Label { get; set; }

        [Required]
        public string Pressing { get; set; }

        [Required]
        public string Rating { get; set; }

        [Required]
        public int Discs { get; set; }

        [Required]
        public string Media { get; set; }

        public DateTime? Bought { get; set; }

        public decimal? Cost { get; set; }

        public string? CoverName { get; set; }

        public string? Review { get; set; }

        #endregion
        
        #region " Navigation Properties "

        public Artist Artist { get; set; }

        #endregion
    }   
}
