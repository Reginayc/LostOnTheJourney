using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LOSTONTHEJOURNEY.Controllers;
using LOSTONTHEJOURNEY.Models;
using LOSTONTHEJOURNEY.Models.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LostOnTheJourney.Tests.Controllers
{
    [TestClass]
    public class MoviesControllerTests
    {
        private MoviesController _controller;
        private LostOnTheJourneyContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<LostOnTheJourneyContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new LostOnTheJourneyContext(options);
            _controller = new MoviesController(_context);
        }

        [TestMethod]
        public async Task GetMovies_ReturnsEmptyList_WhenNoMovies()
        {
            // Act
            var result = await _controller.GetMovies();

            // Assert
            var actionResult = result.Result as OkObjectResult;
            actionResult.Should().NotBeNull();
            var movies = actionResult.Value as List<Movie>;
            movies.Should().BeEmpty();
        }

        [TestMethod]
        public async Task AddMovie_AddsMovieToDatabase()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Description = "Test Description",
            };

            // Act
            var result = await _controller.AddMovie(movie);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            var addedMovie = createdAtActionResult.Value as Movie;
            addedMovie.Should().NotBeNull();
            addedMovie.Title.Should().Be("Test Movie");
            addedMovie.Description.Should().Be("Test Description");
        }

        [TestMethod]
        public async Task GetMovie_ReturnsMovie_WhenMovieExists()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Description = "Test Description"
            };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetMovie(movie.Id);

            // Assert
            var actionResult = result.Result as OkObjectResult;
            actionResult.Should().NotBeNull();
            var returnedMovie = actionResult.Value as Movie;
            returnedMovie.Should().NotBeNull();
            returnedMovie.Id.Should().Be(movie.Id);
            returnedMovie.Title.Should().Be(movie.Title);
            returnedMovie.Description.Should().Be(movie.Description);
        }

        [TestMethod]
        public async Task UpdateMovie_UpdatesMovieInDatabase()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Description = "Test Description"
            };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var updatedMovie = new Movie
            {
                Id = movie.Id,
                Title = "Updated Test Movie",
                Description = "Updated Test Description",
            };

            // Act
            var result = await _controller.UpdateMovie(movie.Id, updatedMovie);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            var dbMovie = await _context.Movies.FindAsync(movie.Id);
            dbMovie.Title.Should().Be("Updated Test Movie");
            dbMovie.Description.Should().Be("Updated Test Description");
        }

        [TestMethod]
        public async Task DeleteMovie_RemovesMovieFromDatabase()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Description = "Test Description",
            };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteMovie(movie.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            var dbMovie = await _context.Movies.FindAsync(movie.Id);
            dbMovie.Should().BeNull();
        }
    }
}
