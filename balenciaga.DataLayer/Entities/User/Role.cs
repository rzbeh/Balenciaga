using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace balenciaga.DataLayer.Entities.User
{
    // this is our user roles models 
    public class Role
    {
        public Role()
        {
                
        }

        [Key]
        public int RoleId { get; set; }

        [Display(Name = "")]
        [Required (ErrorMessage ="please enter {0}")]
        [MaxLength(200 , ErrorMessage = "its not valid")] 
        public string RoleTitle { get; set; }

        public bool IsDelete { get; set; }


        #region Relations
        public virtual List<UserRoles> userRoles { get; set; }



        #endregion
    }
}
