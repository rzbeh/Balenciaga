using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using balenciaga.Core.DTOs;
using balenciaga.Core.Services.Interfaces;
using balenciaga.DataLayer.Entities.User;

namespace balenciaga.Web.Pages.Admin.Roles
{
    public class IndexModel : PageModel
    {
        private IPermissionService _permissionService;

        public IndexModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public List<Role> RolesList { get; set; }


        public void OnGet()
        {
           RolesList = _permissionService.GetRoles();
        }

       
    }
}