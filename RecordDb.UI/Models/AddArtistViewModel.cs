using System.ComponentModel.DataAnnotations;

namespace RecordDb.UI.Models
{
    public class AddArtistViewModel
    {
        public int ArtistId { get; set; }

        public string? FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public string? Biography { get; set; }
    }
}
