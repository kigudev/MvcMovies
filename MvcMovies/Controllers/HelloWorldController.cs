using Microsoft.AspNetCore.Mvc;
using MvcMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovies.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: /HelloWorld
        public IActionResult Index()
        {
            ViewData["Title"] = "Hello world from the controller";
            ViewData["MyVariable"] = 5;
            //return View("/Views/Home/Index.cshtml"); // utiliza otro view
            return View(); // regresa la vista que está ubicada por default en /views/[controlador]/[método]
        }

        // GET: /Helloworld/Welcome
        // GET: /Helloworld/Welcome?name=5&numTimes=5
        public string Welcome(string name, int numTimes = 1)
        {
            return $"Welcome to MVC, {name}, with numTimes = {numTimes}";
        }

        // GET: /Helloworld/WelcomeAgain?name=rene&id=5
        // GET: /Helloworld/WelcomeAgain/5?name=rene
        public IActionResult WelcomeAgain(string name, int id)
        {
            ViewData["Name"] = name;
            ViewData["Id"] = id;

            return View();
        }
    }
}
