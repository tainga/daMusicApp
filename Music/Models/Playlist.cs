using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Music.Models
{
    public class Playlist
    {
        public int PlaylistID { get; set; }
        [Required(ErrorMessage = "Playlist name is required")]
        public string Name { get; set; }
        public IEnumerable<Album> Albums { get; set; }
    }
}