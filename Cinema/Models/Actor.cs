using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} of the actor is required")]
        public string Name { get; set; }
        [StringLength(20, ErrorMessage ="{0} can't have more than {1} characters")]
        public string Lastname { get; set; }
        [Range(1,120, ErrorMessage ="{0} must be between {1} and {2}")]
        public int Age { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
    }
}
