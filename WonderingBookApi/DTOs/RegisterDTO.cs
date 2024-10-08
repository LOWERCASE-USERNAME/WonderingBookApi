﻿using System.ComponentModel.DataAnnotations;

namespace WonderingBookApi.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
