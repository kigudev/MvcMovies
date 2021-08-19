using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovies.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Display(Name="Nombre")]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int OscarsWon { get; set; }
    }
}
