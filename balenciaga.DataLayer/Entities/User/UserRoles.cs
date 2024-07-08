using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace balenciaga.DataLayer.Entities.User
{
    public class UserRoles
    {

        public UserRoles()
        {
                
        }

        [Key]
        public int UR_Id { get; set; }
        public int User_Id { get; set; }
        public int Role_Id { get; set;}


        #region Relations

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        #endregion

    }


}
