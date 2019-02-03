using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleWAF.Models;

namespace SampleWAF.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;          // singleton representation of the database

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string searchString, string movieGenre)
        {
            IQueryable<string> genreQuery = from m in _context.Movie orderby m.Genre select m.Genre;   // mixed-up SQL syntax query, needed to collect all movie genres in database
            var movies = from m in _context.Movie select m;                                            // mixed-up SQL syntax query

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));      // LINQ query to gather the movies whose title contains the given string
            }

            if (!String.IsNullOrEmpty((movieGenre)))
            {
                movies = movies.Where(s => s.Genre == movieGenre);               // LINQ query to gather the chosen genre
            }

            var movieGenreVM = new MovieGenreViewModel();
            movieGenreVM.genres = new SelectList(await genreQuery.Distinct().ToListAsync());           // picking all distinct movie genres for the selectlist
            movieGenreVM.movies = await movies.ToListAsync();
            //return View(await _context.Movie.ToListAsync());           // would return this in default case
            //return View(await movies.ToListAsync());                   // would return this if only the title filtering was available
            return View(movieGenreVM);                   // pass the viewmodel to the view when using one for anything
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)          // integers are always required to be nullable (marked by "?") in a controller method
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .SingleOrDefaultAsync(m => m.ID == id);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);                             // add the new object to the database...
                await _context.SaveChangesAsync();               // ...and save all changes
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

            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);                  // update the database with the given object...
                    await _context.SaveChangesAsync();       // ...and save all changes
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.ID))
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
                .SingleOrDefaultAsync(m => m.ID == id);
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
            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);
            _context.Movie.Remove(movie);                        // remove the chosen object from the database...
            await _context.SaveChangesAsync();                   // ...and save all changes
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
