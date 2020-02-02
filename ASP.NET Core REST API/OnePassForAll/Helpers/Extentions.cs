using Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnePassForAll.Helpers
{
    public static class Extentions
    {
        internal static int GetUserId(ClaimsPrincipal user)
        {
            int userId = -1;
            try
            {
                userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type.Equals(CustomClaimTypes.ID)).Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return userId;
        }
    }
}
