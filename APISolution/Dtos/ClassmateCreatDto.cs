using System;
using System.ComponentModel.DataAnnotations;

namespace APISolution.Dtos
{
    public class ClassmateCreatDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [MaxLength(250)]
        public string Phrase { get; set; }
    }
}

