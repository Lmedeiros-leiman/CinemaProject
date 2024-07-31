using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaProject.Data.Models;

namespace CinemaProject.Data.Services
{
    interface ISessionService
    {
        public Task CreateSession(Session newSession);
    }
    public class SessionService(ApplicationDbContext context) : ISessionService
    {
        public readonly ApplicationDbContext _context = context;
        public async Task CreateSession(Session newSession) {
            await _context.AddAsync(newSession);
        }
    }
}