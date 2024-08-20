using System.ComponentModel.DataAnnotations;
namespace CinemaProject.Data.Models
{
    public class Ticket(ApplicationUser user, Session session)
    {
        [Key]
        public long Id { get; set; }
        public required ApplicationUser User { get; set; } = user;
        public required Session Session { get; set; } = session;
    }
}