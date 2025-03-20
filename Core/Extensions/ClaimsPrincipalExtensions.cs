using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    // Kullanıcı rollerini almak için yazıldı.

    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdStr = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdStr, out var userId) ? userId : 0;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user?.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static List<string> GetRoles(this ClaimsPrincipal user)
        {
            return user?.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();
        }

        public static bool HasRole(this ClaimsPrincipal user, string role)
        {
            return user?.FindAll(ClaimTypes.Role)?.Any(c => c.Value == role) ?? false;
        }
    }
}
