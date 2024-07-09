using balenciaga.Core.DTOs;
using balenciaga.Core.Services.Interfaces;
using balenciaga.DataLayer.Entities.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace balenciaga.web.Areas.UserPanel.Controllers
{

    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private IUserService _userService;
        public WalletController(IUserService userService)
        {
                _userService = userService;
        }

        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {
            ViewBag.ListWallet = _userService.GetWalletUser(User.Identity.Name);
            return View();
        }

        [Route("UserPanel/Wallet")]
        [HttpPost]
        public ActionResult Index(ChargeWalletViewModel charge)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ListWallet = _userService.GetWalletUser(User.Identity.Name);
                return View(charge);

            }
           int WalletId =  _userService.ChargeWallet(User.Identity.Name, charge.Amount, "charging account");

            #region Online Payment

            var payment = new ZarinpalSandbox.Payment(charge.Amount);
            var res = payment.PaymentRequest("Sharge Walet" , "https://localhost:7022/OnlinePayment/" + WalletId);

            if ( res.Result.Status == 100)
            {
                return Redirect("https://sandbox.Zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }

            #endregion
            return null;
        }
    }
}
