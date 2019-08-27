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
        /// <summary>
        /// Adds a game asyncronously
        /// </summary>
        /// <param name="g">The entire video game </param>
        /// <param name="context">The database context</param>
        /// <returns></returns>
        public static async Task<VideoGame> AddAsync(VideoGame g, GameContext context)
        {
            await context.AddAsync(g);
            await context.SaveChangesAsync();
            return g;
        }

        /// <summary>
        /// Searches for games that match the criteria and
        /// returns all games that match
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="criteria">The search criteria for the database</param>
        /// <returns></returns>
        public static async Task<List<VideoGame>> Search(GameContext context, SearchCriteria criteria)
        {
            // SELECT * FROM VideoGames
            // Does NOT query the database
            IQueryable<VideoGame> allGames =
                    from g in context.VideoGames
                    select g;
            if (criteria.MinPrice.HasValue)
            {
                // Add to WHERE clause
                allGames = from g in allGames
                           where g.Price >= criteria.MinPrice
                           select g;
            }

            if (criteria.MaxPrice.HasValue)
            {
                allGames = from g in allGames
                           where g.Price <= criteria.MaxPrice
                           select g;
            }

            if (!string.IsNullOrWhiteSpace(criteria.Title))
            {
                allGames = from g in allGames
                           where g.Title.StartsWith(criteria.Title)
                           select g;
            }

            if (!string.IsNullOrWhiteSpace(criteria.Rating))
            {
                allGames = from g in allGames
                           where g.Rating == criteria.Rating
                           select g;
            }
            // Send final query to the database to return results
            return await allGames.ToListAsync();
        }

        /// <summary>
        /// Returns the total number of pages needed to 
        /// have <paramref name="pageSize" /> amount of products per page
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<int> GetTotalPages(GameContext context, int pageSize)
        {
            int totalNumGames = await context.VideoGames.CountAsync();
            double pages = (double)totalNumGames / pageSize;
            return Convert.ToInt32(Math.Ceiling(pages));
        }

        /// <summary>
        /// Gets all the games in the database
        /// </summary>
        /// <param name="context">The database context</param>
        /// <returns></returns>
        public static async Task<List<VideoGame>> GetAllGames(GameContext context)
        {
            List<VideoGame> games =
                await (from vidGame in context.VideoGames
                 orderby vidGame.Title ascending
                 select vidGame).ToListAsync();

            return games;
        }

        /// <summary>
        /// Gets the game that you are looking for by Id
        /// </summary>
        /// <param name="id">The id of the game</param>
        /// <param name="context">The database context</param>
        /// <returns></returns>
        public static async Task<VideoGame> GetGameById(int id, GameContext context)
        {
            VideoGame g =
                await (from game in context.VideoGames
                 where game.Id == id
                 select game).SingleOrDefaultAsync();

            return g;
        }

        /// <summary>
        /// Updates video game in the database
        /// </summary>
        /// <param name="g">The entire video game</param>
        /// <param name="context">The database context</param>
        /// <returns></returns>
        public static async Task<VideoGame> UpdateGame(VideoGame g, GameContext context)
        {
            context.Update(g);
            await context.SaveChangesAsync();
            return g;
        }

        /// <summary>
        /// Deletes a video game by the games id
        /// </summary>
        /// <param name="id">The video game's id</param>
        /// <param name="context">The database context</param>
        /// <returns></returns>
        public static async Task DeleteById(int id, GameContext context)
        {
            VideoGame g = new VideoGame()
            {
                Id = id
            };

            context.Entry(g).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns 1 page worth of products.
        /// Products are sorted alphabetically by title
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="pageNum">The page number for the products</param>
        /// <param name="pageSize">The number of products per page</param>
        /// <returns></returns>
        public static async Task<List<VideoGame>> GetGamesByPage(GameContext context, int pageNum, int pageSize)
        {
            List<VideoGame> games =
                await context.VideoGames
                            .OrderBy(vg => vg.Title)
                            .Skip((pageNum - 1)* pageSize)
                            .Take(pageSize)
                            .ToListAsync();
            return games;
        }
    }
}
