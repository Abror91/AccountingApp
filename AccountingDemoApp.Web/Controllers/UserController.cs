using AccountingDemoApp.BLL.Interfaces;
using AccountingDemoApp.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AccountingDemoApp.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _users;
        public UserController()
        {

        }
        public UserController(IUserService users)
        {
            _users = users;
        }
        public async Task<ActionResult> UserInfo()
        {
            var userId = User.Identity.GetUserId();
            var user = await _users.GetUserById(userId);
            var u = new UserViewModel
            {
                Id = user.Id,
                Name = user.Email,
                Deposit = user.Deposit
            };
            return View(u);
        }

        public ActionResult DepositMoney(string message)
        {
            ViewBag.UserId = User.Identity.GetUserId();
            if (!string.IsNullOrWhiteSpace(message))
            {
                ViewBag.ErrorMessage = message;
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DepositMoney(DepositViewModel model, string userId)
        {
            var result = await _users.DepositMoney(model.Amount, userId);
            if (result.Succeeded)
                return RedirectToAction("UserInfo");
            return RedirectToAction("DepositMoney", new { message = result.Message });
        }
    }
}