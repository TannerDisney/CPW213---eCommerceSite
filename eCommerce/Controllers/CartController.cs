using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly GameContext _context;

        public CartController(GameContext context)
        {
            _context = context;
        }
        // Add the game to a cookie
        public async Task<IActionResult> Add(int id)
        {
            VideoGame g = await VideoGameDb.GetGameById(id, _context);
            return View();
        }
    }
}