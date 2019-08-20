using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    /// <summary>
    /// Helper class to provide session management
    /// </summary>
    public static class SessionHelper
    {
        private const string MemberIdKey = "MemberId";
        private const string UsernameKey = "Username";
        public static void LogUserIn(IHttpContextAccessor context, int memberId, string username)
        {
            context.HttpContext.Session.SetInt32(MemberIdKey, memberId);
            context.HttpContext.Session.SetString(UsernameKey, username);
        }

        public static bool IsUserLoggedIn(IHttpContextAccessor context)
        {
            if (context.HttpContext.Session.GetInt32(MemberIdKey).HasValue)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Destroys current users session
        /// </summary>
        /// <param name="context"></param>
        public static void LogUserOut(IHttpContextAccessor context)
        {
            context.HttpContext.Session.Clear();
        }

        public static string GetUsername(IHttpContextAccessor context)
        {
            return context.HttpContext.Session.GetString(UsernameKey);
        }

        public static int? GetMemberId(IHttpContextAccessor context)
        {
            return context.HttpContext.Session.GetInt32(MemberIdKey);
        }
    }
}
