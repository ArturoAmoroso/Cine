using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Entities
{
    public class ActorEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
       
        public string Lastname { get; set; }
     
        public int Age { get; set; }
        public ICollection<MovieEntity> Movies { get; set; }
    }
}
