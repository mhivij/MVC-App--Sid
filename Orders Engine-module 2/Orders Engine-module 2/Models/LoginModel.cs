using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orders_Engine_module_2.Models
{
    public class LoginModel
    {
        public string ReturnUrl { get; internal set; }

        public class Login
        {
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [HiddenInput]
            public string ReturnUrl { get; set; }
        }
    }
}