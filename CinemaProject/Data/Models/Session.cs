using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaProject.Data.Models
{
    [Table("Sessions")]
    public class Session() {
        [Key] public int Id {get; set;}
        public long SessionDate {get;set;}
        public string SessionTime {get;set;} = string.Empty;
        public long SessionPrice {get;set;}
    }
}