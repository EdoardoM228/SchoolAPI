using System;
using System.ComponentModel.DataAnnotations;

namespace APISolution.Models
{
    public class Classmate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [MaxLength(250)]
        public string Phrase { get; set; }
    }
}

