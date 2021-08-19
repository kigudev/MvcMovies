using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovies.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TheatersController : Controller
    {
        public string Index()
        {
            return "Listado de cines";
        }
    }
}
