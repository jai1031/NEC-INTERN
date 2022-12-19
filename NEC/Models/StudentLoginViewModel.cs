using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NEC.Models
{
    public class StudentLoginViewModel
    {
        [Required(ErrorMessage = "UserName is required!!")]
        [DisplayName("Mail-ID")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter your password here!!"), StringLength(16, MinimumLength = 6, ErrorMessage = "Your entered password must be in length 6 to 16")]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}