﻿using System.ComponentModel.DataAnnotations;

namespace workoutApp.Models
{
    public class UserInsertRequestModel
    {
        [EmailAddress(ErrorMessage = "Email is not a valid format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{8,}",
            ErrorMessage = "Your password must be at least 8 characters, have at least one upper case letter, " +
            "one lower case letter, one number, and one special character (!@#$%^&*)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password Confirmation is required.")]
        [Compare("Password", ErrorMessage = "Your password must match.")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "IsConfirmed is required.")]
        public bool IsConfirmed { get; set; }
    }
}