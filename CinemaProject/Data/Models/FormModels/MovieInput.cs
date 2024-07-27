using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace CinemaProject.Data.Models.FormModels
{
    public class MovieInput {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; } = null;
        public string?/*List<string>*/ Categories { get; set; }
        public IBrowserFile? PosterImage { get; set; }
        //public List<Attachment> MovieExtras { get; set; } = [];

        //public List<Review> MovieReviews {get;set;} = [];
    }
}