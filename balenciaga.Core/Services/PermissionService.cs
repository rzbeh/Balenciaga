using balenciaga.Core.Services.Interfaces;
using balenciaga.DataLayer.Context;
using balenciaga.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace balenciaga.Core.Services
{
    public class PermissionService : IPermissionService
    {
        BalenciagaContext _context;

        public PermissionService(BalenciagaContext context)
        {
                _context = context;
        }


        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();

        }

        public void AddRolesToUser(List<int> rolesIds, int userId)
        {
            foreach (int roleId in rolesIds)
            {
                _context.UserRoles.Add(new UserRoles()
                {
                    Role_Id = roleId,
                    User_Id = userId    

                });
            }
            _context.SaveChanges();
        }

        public void EditRolesUser(List<int> rolesIds, int userId)
        {
            //delete all rows
           _context.UserRoles.Where(r => r.User_Id == userId).ToList().ForEach(r => _context.UserRoles.Remove(r));

            //add new role
            AddRolesToUser(rolesIds, userId);
        }
    }
}
