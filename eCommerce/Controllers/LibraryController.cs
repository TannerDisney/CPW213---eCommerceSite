using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class LibraryController : Controller
    {
        private readonly GameContext _context;

        public LibraryController(GameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Search(SearchCriteria criteria)
        {
            if (ValidSearch(criteria))
            {
                criteria.GameResults = await VideoGameDb.Search(_context, criteria);
            }
            return View(criteria);
        }

        private bool ValidSearch(SearchCriteria criteria)
        {
            if (criteria.Title == null && 
                criteria.Rating == null && 
                criteria.MinPrice == null && 
                criteria.MaxPrice == null)
            {
                return false;
            }
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            // Null Coalescing Operator
            // if id is not null set page to it, or if null set to 1
            int page = id ?? 1; // the id of the page
            const int PageSize = 3;
            List<VideoGame> games = 
                await VideoGameDb.GetGamesByPage(_context, page, 3);

            int totalPages = 
                await VideoGameDb.GetTotalPages(_context, PageSize);
            ViewData["Pages"] = totalPages;
            ViewData["CurrentPage"] = page;

            return View(games);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(VideoGame game)
        {
            if (ModelState.IsValid)
            {
                // Add to Database
                await VideoGameDb.AddAsync(game, _context);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = true;
            }

            // Return view with model including error messages
            return View(game);
        }

        public async Task<IActionResult> Update(int id)
        {
            VideoGame game = await VideoGameDb.GetGameById(id, _context);

            return View(game);
        }

        [HttpPost]
        public async Task<IActionResult> Update(VideoGame g)
        {
            if (ModelState.IsValid)
            {
                await VideoGameDb.UpdateGame(g, _context);
                return RedirectToAction("Index");
            }

            return View(g);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            VideoGame game =
                await VideoGameDb.GetGameById(id, _context);

            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await VideoGameDb.DeleteById(id, _context);
            return RedirectToAction("Index");
        }
    }
}