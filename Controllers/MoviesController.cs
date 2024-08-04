using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LOSTONTHEJOURNEY.Models;
using LOSTONTHEJOURNEY.Models.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LOSTONTHEJOURNEY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly LostOnTheJourneyContext _context;

        public MoviesController(LostOnTheJourneyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

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

        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(Movie movie)
        {
            movie.Slug = GenerateSlug(movie.Title);
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, Movie updatedMovie)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            movie.Title = updatedMovie.Title;
            movie.Description = updatedMovie.Description;
            movie.Slug = GenerateSlug(updatedMovie.Title);

            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

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

        private string GenerateSlug(string title)
        {
            string str = title.ToLower();
            str = Regex.Replace(str, @"\s+", "-"); // Replace spaces with hyphens
            str = Regex.Replace(str, @"[^a-z0-9\-]", ""); // Remove invalid characters
            return str;
        }
    }
}
