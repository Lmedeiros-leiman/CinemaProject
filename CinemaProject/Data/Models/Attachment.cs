using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaProject.Data.Models
{
    [Table("Attachments")]
    public class Attachment()
    {
        [Key]
        public long Id {get; private set;}
        public string URLPath {get;set;} = String.Empty;
        public string Name { get;set; } = String.Empty;
        public string ContentType {get;set;} =  String.Empty;
    }
}