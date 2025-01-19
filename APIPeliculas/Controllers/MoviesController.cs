using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Models;

namespace APIPeliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesContext _context;

        public MoviesController(MoviesContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            //return await _context.Movies.ToListAsync();
            return await _context.Movies.Include(m => m.Genre)
            .Where(m => m.Active)
            .ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("desactive/{id}")]
        public async Task<IActionResult> DesactiveMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound("Pelicula no encontrada");
            }
            movie.Active = false;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Movie>>> SearchMovie(string? title, int? genreID)
        {
            //return await _context.Movies.ToListAsync();
            var movieQuery = _context.Movies.Include(m => m.Genre)
                .Where(m => m.Active)
                .AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                movieQuery = movieQuery.Where(m => m.Title.Contains(title));
            }
            if (genreID > 0)
            {
                movieQuery = movieQuery.Where(m => m.GenreId == genreID);
            }
            var movies = await movieQuery.ToListAsync();
            if (!movies.Any())
            {
                return NotFound("No se encontraron peliculas");
            }
            return Ok(movies);
        }
        [HttpGet("titles")]
        public async Task<ActionResult<IEnumerable<Object>>> GetTitle()
        {
            //return await _context.Movies.ToListAsync();
            return await _context.Movies.Include(m => m.Genre)
                .Where(m => m.Active)
                .Select(m => new
                {
                    m.Id,
                    m.Title,
                }).ToListAsync();
        }
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
