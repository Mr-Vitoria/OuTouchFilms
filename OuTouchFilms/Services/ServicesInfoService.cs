using Microsoft.EntityFrameworkCore;
using OuTouchFilms.Models;

namespace OuTouchFilms.Services
{
    public static class ServicesInfoService
    {

        private static readonly CookieOptions cookieOpt = new CookieOptions()
        {
            Path = "/",
            Expires = DateTimeOffset.UtcNow.Date.AddDays(2),
            IsEssential = true,
            HttpOnly = false,
            Secure = false
        };
        public static async Task<bool> AddCountFilmsVisit(OuTouchDbContext context,HttpContext httpContext)
        {
            ServicesInfo info = await context.ServicesInfo.FirstOrDefaultAsync(si => si.Date == DateOnly.FromDateTime(DateTime.Now));

            if (httpContext.Request.Cookies["session"] != null)
            {
                return false;
            }
            httpContext.Response.Cookies.Append("session", "nvjdgrfk73bfk63383-384ufr-39tjvnfg", cookieOpt);

            if (info != null)
            {
                info.countVisitsFilm++;
                context.ServicesInfo.Update(info);
            }
            else
            {
                info = new ServicesInfo();
                info.countVisitsFilm++;
                info.Date = DateOnly.FromDateTime(DateTime.Now);
                await context.ServicesInfo.AddAsync(info);
            }
            await context.SaveChangesAsync();
            return true;
        }
    }
}
