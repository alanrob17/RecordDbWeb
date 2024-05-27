namespace RecordDb.API.Models.DTO
{
    public class UpdateRecordDto
    {
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
    }
}
