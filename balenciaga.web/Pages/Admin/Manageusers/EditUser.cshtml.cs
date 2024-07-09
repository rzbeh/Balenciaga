using balenciaga.Core.DTOs;
using balenciaga.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace balenciaga.web.Pages.Admin.Manageusers
{
    public class EditUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;


        public EditUserModel(IUserService userService , IPermissionService permissionService)
        {
                _userService = userService;
            _permissionService = permissionService; 
        }

        [BindProperty]
        public EditUserViewModel EditUserViewModel { get; set; }

        public void OnGet(int id)
        {
            EditUserViewModel = _userService.GetUserForShowInEditMode(id);
            ViewData["Roles"] = _permissionService.GetRoles();
        }
        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            _userService.EditUserFromAdmin(EditUserViewModel);

            //edit roles 
            _permissionService.EditRolesUser(SelectedRoles , EditUserViewModel.UserId);

            return RedirectToPage("Index");
        }
    }
}
 