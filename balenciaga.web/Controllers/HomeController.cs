using balenciaga.Core.Services.Interfaces;
using balenciaga.web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace balenciaga.web.Controllers
{
    public class HomeController : Controller
    {
        IUserService _userService; 
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger , IUserService userService)
        {
            _logger = logger;
            _userService = userService;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "Ok" &&
                HttpContext.Request.Query["Authority"] != "" 
                )

            {
                string authority = HttpContext.Request.Query["Authority"];
                var Wallet = _userService.GetWalletByWalletId(id);
                var payment = new ZarinpalSandbox.Payment(Wallet.Amount);
                var res = payment.Verification(authority).Result;

                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true; 
                    Wallet.IsPay = true;
                    _userService.UpdateWallet(Wallet);  
                }
            }
            return View();

        }


    }
}
