
using Microsoft.EntityFrameworkCore;
using CinemaProject.Data.Models;
namespace CinemaProject.Data.Services;

    public interface IMovieService
    {
        public Task<List<Movie>> GetAllMovies();
        public Task<Movie?> GetMovieById(long id);

        // sesions related
        public Task<Boolean> CreateMovieSession(Movie targetMovie, Session sessionDetails);

        public async Task<List<Movie>> GetMoviesWithSessions();
        //
        // Crud Operations
        public Task<Movie> CreateMovie(Movie newMovie);
        public  Task<Movie?> RemoveMovie(long id);
        public  Task<Movie?> ModifyMovie(long id, Movie modifiedMovie);
        public  Task<bool> DeleteMovie(Movie targetMovie);
    }

    public class MovieService(ApplicationDbContext context, IAttachmentService attachment,ILogger<MovieService> logger) : IMovieService   {
        private readonly ApplicationDbContext _context = context;
        private readonly IAttachmentService _attachment = attachment;
        private readonly ILogger _logger = logger;

        public async Task<List<Movie>> GetAllMovies() {
            return await _context.Movies
                            //.Include(s => s.movieSessions)
                            .Include(p => p.PosterImage)
                            .Include(p => p.Sessions)
                            .Include(p => p.MovieExtras)
                            .ToListAsync();
        }

        
        public async Task<List<Movie>> GetMoviesWithSessions() {
            return await _context.Movies.Where(m => m.Sessions.Count > 0)
                            .Include(p => p.PosterImage)
                            .Include(p => p.Sessions)
                            .Include(p => p.MovieExtras)
                            .ToListAsync();
        }
        
        public async Task<Movie?> GetMovieById(long id) {
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
        public async Task<Movie?> RemoveMovie(long id) {
            // sets the visibility to false.
            var entry = await _context.Movies.FindAsync(id);
            if (entry == null) { return null;}
            entry.SetActive(false);
            await _context.SaveChangesAsync();

            return entry;
        }
        public async Task<Movie?> ModifyMovie(long id,Movie updatedMovie) {
            var entry = await _context.Movies.FindAsync(updatedMovie.Id);
            if (entry == null) { 
                _logger.LogInformation("Failure modifying movie. ID not found: " + updatedMovie.Id.ToString() );
                return null; 
            }
            
            entry.Title = updatedMovie.Title;
            entry.Description = updatedMovie.Description;
            entry.Categories = updatedMovie.Categories;
            entry.ReleaseDate = updatedMovie.ReleaseDate;
            entry.PosterImage = updatedMovie.PosterImage;
            entry.MovieExtras = updatedMovie.MovieExtras;
            
            await _context.SaveChangesAsync();
            _logger.LogInformation("Sucessfully modified movie.");
            return entry;
        }
        public async Task<bool> DeleteMovie(Movie targetMovie) {
            
            var databaseEntry = await _context.Movies.FindAsync(targetMovie.Id);
            if (databaseEntry == null) { 
                _logger.LogInformation("Failure attempting to delete movie, not found ID. " + targetMovie.Id.ToString());
                return false;
            }

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
            _logger.LogInformation("Sucessfully delete movie from database.");
            return true;
        }
    
        
        // session Related
        public async Task<Boolean> CreateMovieSession(Movie targetMovie, Session sessionDetails) {
            if (targetMovie.Id == 0) return false;

            var databaseMovie = await GetMovieById(targetMovie.Id);
            if (databaseMovie == null) return false;

            databaseMovie.Sessions.Add(sessionDetails);

            await _context.SaveChangesAsync();
            return true;
        }

    
    
    }
