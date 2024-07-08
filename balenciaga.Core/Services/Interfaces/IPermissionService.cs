using balenciaga.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace balenciaga.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region roles
        List<Role> GetRoles();
        void AddRolesToUser(List<int> rolesIds , int userId);
        void EditRolesUser(List<int> rolesIds , int userId);
        #endregion
    }
}
    