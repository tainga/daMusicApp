using System.ComponentModel.DataAnnotations;

namespace Music.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }
        [Required(ErrorMessage = "Artist name is required")]
        [StringLength(20, ErrorMessage = "Artist name cannot be longer than 20 characters.")]
        public string Name { get; set; }
    }
}