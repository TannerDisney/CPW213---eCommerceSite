using eCommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Data
{
    /// <summary>
    /// Db helper class for video games
    /// </summary>
    public static class VideoGameDb
    {
        public static async Task<VideoGame> AddAsync(VideoGame g, GameContext context)
        {
            await context.AddAsync(g);
            await context.SaveChangesAsync();
            return g;
        }

        public static async Task<List<VideoGame>> GetAllGames(GameContext context)
        {
            List<VideoGame> games =
                await (from vidGame in context.VideoGames
                 orderby vidGame.Title ascending
                 select vidGame).ToListAsync();

            return games;
        }
    }
}
