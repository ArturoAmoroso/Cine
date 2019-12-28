using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Movie needs a {0}")]
        public string Name { get; set; }
        public int Duration { get; set; }
        [StringLength(20, ErrorMessage = "{0} can't have more than {1} characters")]
        public string Genre { get; set; }
        //[JsonIgnore]
        public int ActorId { get; set; }
    }
}