using balenciaga.Core.DTOs;
using balenciaga.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace balenciaga.web.Pages.Admin.Manageusers
{
    public class ListDeleteUsersModel : PageModel
    {
        private IUserService _userService;

        public ListDeleteUsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public UsersForAdminViewModel UserForAdminViewModel { get; set; }

        public void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            Console.WriteLine("ListDeleteUsers page accessed");

            UserForAdminViewModel = _userService.GetDeleteUser(pageId, filterEmail, filterUserName);
        }
    }
}
