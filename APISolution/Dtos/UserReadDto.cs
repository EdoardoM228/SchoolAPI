﻿using System;
using System.ComponentModel.DataAnnotations;

namespace APISolution.Dtos
{
    public class UserReadDto
    {
        [MaxLength(30)]
        [Required]
        public string UserName { get; set; }

        [MaxLength(20)]
        [Required]
        public string UserPassword { get; set; }
    }
}
