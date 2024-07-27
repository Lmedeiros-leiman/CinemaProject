using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaProject.Data.Models
{
    public class Attachment()
    {
        [Key]
        public int Id {get; private set;}
        public required string URLPath {get;set;}
        public required string Name { get;set; }
        public required string ContentType {get;set;}

    }
}