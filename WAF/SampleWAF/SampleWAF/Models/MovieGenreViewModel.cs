using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SampleWAF.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie> movies;             // container of movies of the chosen genre
        public SelectList genres;              // container of all genres

        public string MovieGenre { get; set; }      // property of the search string, its private attribute is used in the controller
    }
}
