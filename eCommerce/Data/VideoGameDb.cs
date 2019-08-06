using eCommerce.Models;
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
        public static VideoGame Add(VideoGame g, GameContext context)
        {
            context.Add(g);
            context.SaveChanges();
            return g;
        }
    }
}
