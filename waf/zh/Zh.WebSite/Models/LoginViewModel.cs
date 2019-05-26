﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Zh.WebSite.Models
{
    /// <summary>
    /// Felhasználóval kapcsolatos információk.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Felhasználónév.
        /// </summary>
        [Required(ErrorMessage = "A felhasználónév megadása kötelező.")]
        public String UserName { get; set; }

        /// <summary>
        /// Jelszó.
        /// </summary>
        [Required(ErrorMessage = "A jelszó megadása kötelező.")]
        [DataType(DataType.Password)]        
        public String UserPassword { get; set; }
	}
}