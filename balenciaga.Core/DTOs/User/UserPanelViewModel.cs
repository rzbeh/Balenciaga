using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace balenciaga.Core.DTOs
{
    public class InformationUserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Wallet { get; set; }


    }

    public class SideBarUserPanelViewModel
    {
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }
        public string ImageName { get; set; }

    }

    public class EditProfileViewModel()
    {

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "please enter {0}")]
        [MaxLength(200, ErrorMessage = "its not valid")]
        public string UserName { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "please enter {0}")]
        [MaxLength(200, ErrorMessage = "its not valid")]
        [EmailAddress(ErrorMessage = "email is not valid")]
        public string Email { get; set; }
        public IFormFile UserAvatar { get; set; }
        public string AvatarName { get; set; }
    }
}
