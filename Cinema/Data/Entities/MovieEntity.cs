using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Entities
{
    public class MovieEntity
    {
        [Required]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public int Duration { get; set; }
       
        public string Genre { get; set; }
        public string imgURL { get; set; }
        public int year { get; set; }
        [Required]
        [ForeignKey("ActorId")]
        public virtual ActorEntity Actor { get; set; }
    }
}
