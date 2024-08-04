using Microsoft.AspNetCore.Mvc;
using LOSTONTHEJOURNEY.Models;
using System.Collections.Generic;
using System.Linq;

namespace LOSTONTHEJOURNEY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private static readonly List<Movie> Movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Lost on Journey", Description = "Lost on Journey is a 2010 Chinese comedy film directed by Raymond Yip and starring Xu Zheng and Wang Baoqiang. This film depicts an amusing yet realistic portrayal of the issues prevalent in the Chinese society, especially during the chaotic Chunyun when everyone wants to reunite with their family for the Chinese New Year celebrations.", Slug = "lost-on-journey" }
            // Add more movies as needed
        };

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetMovies()
        {
            return Movies;
        }

        [HttpGet("{id}")]
        public ActionResult<Movie> GetMovie(int id)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

        [HttpPost]
        public ActionResult<Movie> AddMovie(Movie movie)
        {
            movie.Id = Movies.Any() ? Movies.Max(m => m.Id) + 1 : 1;
            movie.Slug = GenerateSlug(movie.Title);
            Movies.Add(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateMovie(int id, Movie updatedMovie)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            movie.Title = updatedMovie.Title;
            movie.Description = updatedMovie.Description;
            movie.Slug = GenerateSlug(updatedMovie.Title);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMovie(int id)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            Movies.Remove(movie);
            return NoContent();
        }

        private string GenerateSlug(string title)
        {
            string str = title.ToLower();
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\s+", "-"); // Replace spaces with hyphens
            str = System.Text.RegularExpressions.Regex.Replace(str, @"[^a-z0-9\-]", ""); // Remove invalid characters
            return str;
        }
    }
}
