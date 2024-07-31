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
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime ReleaseDate { get; set; } = DateTime.FromBinary(0);
        public string Categories { get; set; } = string.Empty;
        public IBrowserFile? PosterImage { get; set; }
    }
}