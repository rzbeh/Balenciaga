using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace balenciaga.DataLayer.Entities.User
{
    public class User
    {
        public User()
        {
                
        }

        [Key]
        public int UserId { get; set; }


        [Display(Name = "User Name")]
        [Required(ErrorMessage = "please enter {0}")]
        [MaxLength(200, ErrorMessage = "its not valid")]
        public string UserName { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "please enter {0}")]
        [MaxLength(200, ErrorMessage = "its not valid")]
        [EmailAddress(ErrorMessage ="email is not valid")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "please enter {0}")]
        [MaxLength(200, ErrorMessage = "its not valid")]
        public string Password { get; set; }

        
        [Display(Name = "Active code")]
        [MaxLength(50, ErrorMessage = "its not valid")]
        public string ActiveCode { get; set; }

       
        [Display(Name = "situation")]
        public bool IsActive { get; set; }
       
        
        [Display(Name = " The Register Date")]
        public DateTime RegisterDate { get; set;}
        
        [Display(Name = "Avatar")]
        [MaxLength(200, ErrorMessage = "its not valid")]    
        public string UserAvatar { get; set;}

        public bool IsDelete { get; set;}


        #region Relations

        public virtual List<UserRoles> UserRoles { get; set; }
        public virtual List<Wallet.Wallet> Wallets { get; set; }

        #endregion

    }
}
