using balenciaga.DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace balenciaga.Core.DTOs
{
    public class UsersForAdminViewModel
    {
        public List<User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }

    public class CreateUserViewModel
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
        public IFormFile UserAvatar { get; set; }
    }

    public class EditUserViewModel
    {
        public int UserId  { get; set; }
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
        public IFormFile UserAvatar { get; set; }
        public List<int> UserRoles { get; set; }
        public string AvatarName { get; set; }


    }
}
