using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcMovies.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie> Movies { get; set; }
        public List<Movie> RecentMovies { get; set; }
        public string Search { get; set; }
        public string Genre { get; set; }
        public SelectList Genres { get; set; }
        public List<string> SelectedRatings { get; set; }
        public List<string> Ratings { get; set; }
    }
}