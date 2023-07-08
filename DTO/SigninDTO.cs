﻿using System.ComponentModel.DataAnnotations;

namespace Hamro_Pasal.DTO
{
    public class SigninDTO
    {


        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
