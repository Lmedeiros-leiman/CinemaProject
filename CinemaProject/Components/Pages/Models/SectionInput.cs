using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CinemaProject.Data.Models;

namespace CinemaProject.Components.Pages.Models
{
    public class SectionInput
    {
        [Key] public long Id {get; set;}
        public Movie TargetMovie {get; set;} = new();
        public long selectedMovieIndex = 0;
        public DateTime SectionDate {get;set;}
        public int SectionTurn {get;set;}
        public long SectionPrice {get;set;}
    }
}