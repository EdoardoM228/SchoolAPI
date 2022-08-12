using System;
using System.ComponentModel.DataAnnotations;

namespace APISolution.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [MaxLength(30)]
        [Required]
        public string UserName { get; set; }

        [MaxLength(20)]
        [Required]
        public string UserPassword { get; set; }
    }
}

