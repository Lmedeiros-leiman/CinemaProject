using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaProject.Data.Models
{
    [Table("Movies")]
    public class Movie
    {
        [Key] public long Id { get; set; }
        public bool Active { get; set; } = true;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long ReleaseDate { get; set; }
        public long InputedDate {get; set;}
        public string Categories { get; set; } = string.Empty;
        public List<Session> Sessions {get; set;} = [];
        public List<Attachment> MovieExtras { get; set; } = [];
        public Attachment PosterImage { get; set; } = new Attachment();

        public Boolean SetActive(Boolean newState ) {
            this.Active = newState;
            return !this.Active;
        }
        public DateTime GetReleaseDate() {
            return DateTime.FromBinary(this.ReleaseDate);
        }
    }
}
