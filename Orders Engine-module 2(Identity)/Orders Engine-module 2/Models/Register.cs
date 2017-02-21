using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Orders_Engine_module_2.Models
{
    public class Register
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password Should be of min 6 characters")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}