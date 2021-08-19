using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovies.Data;
using MvcMovies.Models;

namespace MvcMovies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMoviesContext _context;

        public MoviesController(MvcMoviesContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string search, string genre, List<string> selectedRatings)
        {
            var movieGenres = _context.Movie.Select(c => c.Genre).Distinct();
            var movieRatings = _context.Movie.Select(c => c.Rating).Distinct();
            var movies = _context.Movie.AsQueryable();
            var recentMovies = _context.Movie.OrderByDescending(c => c.ReleaseDate).Take(5);

            if (!string.IsNullOrEmpty(search))
                movies = movies.Where(c => c.Title.Contains(search));

            if (!string.IsNullOrEmpty(genre))
                movies = movies.Where(c => c.Genre == genre);

            if (selectedRatings.Any())
                movies = movies.Where(c => selectedRatings.Contains(c.Rating));

            var vm = new MovieGenreViewModel {
                Movies = await movies.ToListAsync(),
                Genres = new SelectList(await movieGenres.ToListAsync()),
                Ratings = await movieRatings.ToListAsync(),
                SelectedRatings = selectedRatings,
                RecentMovies = await recentMovies.ToListAsync()
            };

            return View(vm);
        }

        [HttpPost]
        public string Index(string search, bool _)
        {
            return $"buscaste {search}";
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // cuando queremos regresar una vista parcial por Ajax o petición
        public IActionResult Recent()
        {
            var recentMovies = _context.Movie.OrderByDescending(c => c.ReleaseDate).Take(5).ToList();
            return PartialView("_RecentMovies", recentMovies);
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
