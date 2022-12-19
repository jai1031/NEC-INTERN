using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NEC.Models
{
    public class AdminLoginModel
    {
        [Required(ErrorMessage = "Enter the Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter the Password")]
        public string Password { get; set; }
    }
}