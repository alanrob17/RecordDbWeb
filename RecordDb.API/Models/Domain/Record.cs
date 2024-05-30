using System.ComponentModel.DataAnnotations;

namespace RecordDb.API.Models.Domain
{
    public class Record
    {
        #region " Properties "

        public int RecordId { get; set; } // identity field

        public int ArtistId { get; set; } // relate to the artist entity

        public string Name { get; set; }

        public string Field { get; set; }

        public int Recorded { get; set; }

        public string Label { get; set; }

        public string Pressing { get; set; }

        public string Rating { get; set; }

        public int Discs { get; set; }

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
