﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Music.Models
{
    public class Album
    {

        public int AlbumID { get; set; }
        [Required(ErrorMessage ="Album title is required")]
        public string Title { get; set; }
        [Display(Name ="Genre")]
        public int GenreID { get; set; }
        public Genre Genre { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Artist")]
        public int ArtistID { get; set; }
        public Artist Artist { get; set; }

    }
}