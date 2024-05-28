using System.ComponentModel.DataAnnotations;

namespace RecordDb.API.Models.Domain
{
    public class Artist
    {
        #region " Properties "

        [Required]
        public int ArtistId { get; set; } // identity field

        public string? FirstName { get; set; }

        [Required]
        public string LastName { get; set; } // not null

        [Required]
        public string Name { get; set; }

        public string? Biography { get; set; }

        #endregion
    }
}
