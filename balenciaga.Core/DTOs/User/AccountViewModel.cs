using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace balenciaga.Core.DTOs
{
    public class RegisterViewModel
    {
        [Display(Name = " User NameS")]
        [Required(ErrorMessage = "please enter youre name")]
        [MaxLength(200, ErrorMessage = "not valid")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "please enter youre Email")]
        [MaxLength(200, ErrorMessage = "ddddd")]
        [EmailAddress(ErrorMessage = " dddd  ")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "please enter youre password")]
        [MaxLength(200, ErrorMessage = "ddddd")]
        public string Password { get; set; }

        [Display(Name = "Repeat password")]
        [Required(ErrorMessage = "please enter RePassword")]
        [MaxLength(200, ErrorMessage = "dddd")]
        [Compare("Password", ErrorMessage = "ddddd")]
        public string RePassword { get; set; }
    }

    public class loginViewModel
    {

        [Display(Name = "Email")]
        [Required(ErrorMessage = "deddd")]
        [MaxLength(200, ErrorMessage = "dddddd")]
        [EmailAddress(ErrorMessage = "ddddddd")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "ddddd")]
        [MaxLength(200, ErrorMessage = "ddddd")]
        public string Password { get; set; }
    }
}
