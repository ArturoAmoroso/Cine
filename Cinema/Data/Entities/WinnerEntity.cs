using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Entities
{
    public class WinnerEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int year { get; set; }
        public string movie { get; set; }
    }
}
