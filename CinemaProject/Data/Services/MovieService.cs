
using Microsoft.EntityFrameworkCore;
using CinemaProject.Data.Models;

namespace CinemaProject.Data.Services;

    public interface IMovieService
    {
        public Task<List<Movie>> GetAllMovies();
        public Task<Movie?> GetMovieByiD(int id);
        //
        // Crud Operations
        public Task<Movie> CreateMovie(Movie newMovie);
        public  Task<Movie?> RemoveMovie(int id);
        public  Task<Movie?> ModifyMovie(int id, Movie modifiedMovie);
        public  Task<bool> DeleteMovie(Movie targetMovie);
    }

    public class MovieService(ApplicationDbContext context, IAttachmentService attachment ,ILogger<MovieService> logger) : IMovieService   {
        private readonly ApplicationDbContext _context = context;
        private readonly IAttachmentService _attachment = attachment;
        private readonly ILogger _logger = logger;

        public async Task<List<Movie>> GetAllMovies() {
            return await _context.Movies.Include(p => p.PosterImage).ToListAsync();
        }

        public async Task<Movie?> GetMovieByiD(int id) {
            return await _context.Movies.FindAsync(id);
        }

        // CRUD movies.
        public  async Task<Movie> CreateMovie(Movie newMovie) {
            newMovie.InputedDate = DateTime.UtcNow.Ticks;

            // insert it into the database
            var newEntry = await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();
            return newEntry.Entity;
        }
        public async Task<Movie?> RemoveMovie(int id) {
            // sets the visibility to false.
            var entry = await _context.Movies.FindAsync(id);
            if (entry == null) { return null;}
            entry.SetActive(false);
            await _context.SaveChangesAsync();

            return entry;
        }
        public async Task<Movie?> ModifyMovie(int id,Movie updatedMovie) {
            var entry = await _context.Movies.FindAsync(id);
            if (entry == null) { return null;}

            entry = updatedMovie;

            await _context.SaveChangesAsync();
            return entry;
        }
        public async Task<bool> DeleteMovie(Movie targetMovie) {
            
            var databaseEntry = await _context.Movies.FindAsync(targetMovie.Id);
            if (databaseEntry == null) { return false;}

            _context.Remove(databaseEntry);

            try
            {
                await _attachment.DeleteFile(databaseEntry.PosterImage);
                foreach(Attachment extra in databaseEntry.MovieExtras) {
                    await _attachment.DeleteFile(extra);
                }
                
            } 
            catch (Exception)
            {
                _logger.LogInformation("Error when trying to delete movie files");
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
