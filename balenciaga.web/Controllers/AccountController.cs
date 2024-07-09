using balenciaga.Core;
using balenciaga.Core.DTOs;
using balenciaga.Core.Services.Interfaces;
using balenciaga.DataLayer.Entities.User;
using BF_Core.Generator;
using BF_Core.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using balenciaga.Core.Convertors;
using balenciaga.Core.Sender;

namespace balenciaga.web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;


        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [Route("Register")]

        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }


            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");
                return View(register);
            }
            if (_userService.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                return View(register);
            }

           DataLayer.Entities.User. User user = new User()
            {
                ActiveCode = NameGenarator.GenerateUniqCode(),
                Email = FixedText.FixEmail(register.Email),
                IsActive = true,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                RegisterDate = DateTime.Now,
                UserAvatar = "Avatar.jpg",
                UserName = register.UserName
            };
            _userService.AddUser(user);

            #region SendActivationEmail


            #endregion

            return View("SuccessRegister", user);
        }
        #endregion

        #region Login

       
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(loginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userService.LoginUser(login);

            if (user != null)
            {
                if (user.IsActive)
                {
                    var calims = new List<Claim>()
                    {
                    new Claim(ClaimTypes.NameIdentifier , user.UserId.ToString()) ,
                    new Claim (ClaimTypes.Name , user.UserName)
                    };

                    var identity = new ClaimsIdentity(calims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(principal);


                    ViewBag.IsSuccess = true;

                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "your account is not active");
                }


            }

            ModelState.AddModelError("Email", "undefind User");

            return View(login);
        }

        #endregion

        #region Active Account

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }

        #endregion

        #region LOgout
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion  
    }
}
