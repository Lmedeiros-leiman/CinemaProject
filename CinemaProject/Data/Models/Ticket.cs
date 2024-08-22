using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaProject.Data.Models
{
    [Table("Tickets")]
    public class Ticket()
    {
        [Key]
        public long Id { get; set; }
        public required ApplicationUser User { get; set; }
        public long PurchaseDate {get; set;} = DateTime.UtcNow.Ticks;
    }
}