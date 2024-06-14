using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ArtSpectrum.Repository.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsInRole(this ClaimsPrincipal user, string role, StringComparison comparisonType)
        {
            return user.Claims.Any(c => c.Type == ClaimTypes.Role && string.Equals(c.Value, role, comparisonType));
        }
    }
}
