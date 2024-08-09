using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using CinemaProject.Data.Models;

namespace CinemaProject.Data;

    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public  required DbSet<Movie> Movies {get; set;}
        public required DbSet<Attachment> Attachments {get; set;}
        public required DbSet<Session> Sessions {get;set;}
        //public required DbSet<MovieSession> MovieSessions {get; set;}
        //public required DbSet<MovieSessionTicket> MovieSessionTickets {get; set;}
        //public required DbSet<Review> Reviews {get; set;}
        
    }
