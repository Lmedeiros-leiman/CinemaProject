using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CinemaProject.Data.Models.FormModels;

namespace CinemaProject.Data.Models
{
    public class Movie
    {
    [Key]
    public int Id {get; private set;}

    public bool Active { get; set;} = true;

    [DefaultValue("Not provided.")]
    public string? Title {get; set; }

    [DefaultValue("Not provided.")]
    public string? Description {get; set;} 

    [Timestamp]
    public DateTime? ReleaseDate { get; set; } = DateTime.UtcNow;

    [DefaultValue("Not provided.")]
    public string?/*List<string>*/ Categories {get; set;}
    public Attachment? PosterImage {get; set;}
    //public List<Attachment> MovieExtras { get; set; } = [];

    //public List<Review> MovieReviews {get;set;} = [];
    }
}