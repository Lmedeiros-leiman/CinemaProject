using CinemaProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaProject.Data.Services
{
    interface ISessionService
    {
        public Task CreateSession(Session newSession);
        public Task<List<Session>> GetSessions();
    }


    public class SessionService(ApplicationDbContext context) : ISessionService
    {
        public readonly ApplicationDbContext _context = context;
        public async Task CreateSession(Session newSession) {
            var newEntry = await _context.AddAsync(newSession);
            
            await _context.SaveChangesAsync();
        }
        public async Task<List<Session>> GetSessions() {
            return await _context.Sessions.ToListAsync();
        }

        
    }
}