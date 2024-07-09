using balenciaga.Core.DTOs;
using balenciaga.Core.Services.Interfaces;
using balenciaga.DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace balenciaga.web.Pages.Admin.Manageusers
{
    public class CreateUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;

        public CreateUserModel(IUserService userService , IPermissionService permissionService)
        {
            _permissionService = permissionService;
            _userService = userService;
        }

        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }

        public void OnGet()
        {
            var roles = _permissionService.GetRoles();
            if (roles == null)
            {
                roles = new List<Role>(); // Handle the case where roles are null
            }
            ViewData["Roles"] = roles;
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
                return Page();

            int userId = _userService.AddUserFromAdmin(CreateUserViewModel);

            // Add roles
            _permissionService.AddRolesToUser(SelectedRoles, userId);


            return Redirect("/Admin/Manageusers");
        }
    }
}   
