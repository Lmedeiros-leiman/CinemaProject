using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public  Task<bool> DeleteMovie(int id);
    }

    public class MovieService(ApplicationDbContext context) : IMovieService   {
        private readonly ApplicationDbContext _context = context;
        public async Task<List<Movie>> GetAllMovies() {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie?> GetMovieByiD(int id) {
            return await _context.Movies.FindAsync(id);
        }

        // CRUD movies.
        public  async Task<Movie> CreateMovie(Movie newMovie) {
            var newEntry = await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();
            return newEntry.Entity;
            
        }
        public async Task<Movie?> RemoveMovie(int id) {
            // sets the visibility to false.
            var entry = await _context.Movies.FindAsync(id);
            if (entry == null) { return null;}
            entry.Active = false;
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
        public async Task<bool> DeleteMovie(int id) {
            var entry = await _context.Movies.FindAsync(id);
            if (entry == null) { return false;}

            _context.Remove(entry);
            await _context.SaveChangesAsync();
            return true;
            
        }
    }
