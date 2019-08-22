using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eCommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly GameContext _context;

        public CartController(GameContext context, IHttpContextAccessor httpAccessor)
        {
            _context = context;
            _httpAccessor = httpAccessor;
        }
        // Add the game to a cookie
        public async Task<IActionResult> Add(int id)
        {
            VideoGame g = await VideoGameDb.GetGameById(id, _context);
            CartHelper.Add(_httpAccessor, g);
            /*
            string data = JsonConvert.SerializeObject(g);
            CookieOptions options = new CookieOptions()
            {
                Secure = true,
                MaxAge = TimeSpan.FromDays(14)
            };

            _httpAccessor.HttpContext.Response.Cookies.Append("CartCookie", data, options);
            */
            return RedirectToAction("Index", "Library");
        }
    }
}