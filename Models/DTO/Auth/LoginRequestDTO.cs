﻿using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO.Auth
{
    public class LoginRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
