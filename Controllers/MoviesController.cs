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
            new Movie { Id = 1, Title = "Lost on Journey", Description = "Lost on Journey is a 2010 Chinese comedy film directed by Raymond Yip and starring Xu Zheng and Wang Baoqiang. This film depicts an amusing yet realistic portrayal of the issues prevalent in the Chinese society, especially during the chaotic Chunyun when everyone wants to reunite with their family for the Chinese New Year celebrations." }
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
            movie.Id = Movies.Max(m => m.Id) + 1;
            Movies.Add(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
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
    }
}

