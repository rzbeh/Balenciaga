using balenciaga.Core.DTOs;
using balenciaga.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace balenciaga.web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {

        private IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_userService.GetUserInformation(User.Identity.Name));
        }

        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
            {

            return View(_userService.GetDataForEditProfileUser(User.Identity.Name));

            }

        [HttpPost("UserPanel/EditProfile")]
        public IActionResult EditProfile(EditProfileViewModel profile)
        {

            if(ModelState.IsValid)
            {
                return View(profile);
            }
            _userService.EditProfile(User.Identity.Name, profile);
            return Redirect("/Login");
        }

       
    }

}
