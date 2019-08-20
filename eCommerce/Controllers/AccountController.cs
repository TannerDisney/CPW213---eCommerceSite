using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly GameContext _context;
        private readonly IHttpContextAccessor _httpAccessor;

        public AccountController(GameContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _httpAccessor = accessor;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Member m)
        {
            if (ModelState.IsValid)
            {
                await MemberDb.Add(_context , m);
                SessionHelper.LogUserIn(_httpAccessor, m.MemberId, m.Username);
                TempData["Message"] = $"Welcome {m.Username} to Tanner's Game Emporium!";
                return RedirectToAction("Index", "Home");
            }

            return View(m);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Member member = await MemberDb.IsLoginValid(model, _context);
                if (member != null)
                {
                    TempData["Message"] = $"Welcome back {model.UsernameOrEmail}!";
                    // Create session for user
                    SessionHelper.LogUserIn(_httpAccessor, member.MemberId, member.Username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Sorry, we are unable to find your credentials within our database.");
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            // Clear all session data
            SessionHelper.LogUserOut(_httpAccessor);
            TempData["Message"] = "Logged out successfully!";
            return RedirectToAction("Index", "Home");
            
        }
    }
}