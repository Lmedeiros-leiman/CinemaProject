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
            await _context.AddAsync(newSession);
        }
        public async Task<List<Session>> GetSessions() {
            return await _context.Sessions.Include(m => m.TargetMovie ).ToListAsync();
        }
    }
}