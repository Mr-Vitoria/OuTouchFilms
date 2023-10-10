using Microsoft.EntityFrameworkCore;
using OuTouchFilms.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace OuTouchFilms.Services
{
    public class UserService : IUserService
    {
        private readonly CookieOptions cookieOpt = new CookieOptions()
        {
            Path = "/",
            Expires = DateTimeOffset.UtcNow.AddDays(31),
            IsEssential = true,
            HttpOnly = false,
            Secure = false
        };
        private readonly OuTouchDbContext context;

        public UserService(OuTouchDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddUser(User user, HttpContext httpContext)
        {
            if (await context.Users.FirstOrDefaultAsync(us => us.Email == user.Email) != null)
            {
                return false;
            }
            user.Password = user.EncryptPasswordBase64();
            user.ImgUrl = "http://outouch.ru/images/NoImageProfile.png";
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            httpContext.Response.Cookies.Append("id", user.Id + "", cookieOpt);
            httpContext.Response.Cookies.Append("image", user.ImgUrl + "", cookieOpt);

            return true;
        }

        public async Task<bool> CheckUser(User checkedUser, HttpContext httpContext)
        {
            var user = await context.Users.FirstOrDefaultAsync(us => us.Email == checkedUser.Email);
            if (user == null ||
                 user.Password != checkedUser.EncryptPasswordBase64())
            {
                return false;
            }
            httpContext.Response.Cookies.Append("id", user.Id + "", cookieOpt);
            httpContext.Response.Cookies.Append("image", user.ImgUrl + "", cookieOpt);

            return true;
        }

        public async Task<object?> GetCurrentUserInfo(HttpContext httpContext, TypeOfUserFilm? TypeOfUserFilm)
        {
            if (httpContext.Request.Cookies.ContainsKey("id"))
            {
                int id = int.Parse(httpContext.Request.Cookies["id"]);
                var user = await context.Users.FindAsync(id);

                await context.Films.LoadAsync();

                return new
                {
                    user = user,
                    userFilms = TypeOfUserFilm == null ? null : await context.UserFilms.Where(f => f.UserId == id && f.TypeOfUserFilm == TypeOfUserFilm).OrderByDescending(f => f.Id).Select(f => f.Film).ToListAsync(),
                    TypeOfUserFilm = TypeOfUserFilm
                };
            }
            return null;
        }



        public bool SignOut(HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete("id");
            httpContext.Response.Cookies.Delete("image");

            return true;
        }


        public async Task<bool> ChangeCurrentUserProperties(HttpContext httpContext, string userImg, string emailSend)
        {
            int id = int.Parse(httpContext.Request.Cookies["id"]);
            var user = await context.Users.FindAsync(id);
            user.ImgUrl = userImg;

            if (emailSend == "on")
            {
                user.NeedEmailSend = true;
            }
            else
            {
                user.NeedEmailSend = false;
            }

            context.Users.Update(user);
            await context.SaveChangesAsync();
            httpContext.Response.Cookies.Delete("image");
            httpContext.Response.Cookies.Append("image", user.ImgUrl + "", cookieOpt);

            return true;

        }

        public async Task<bool> AddComment(int userId, int filmId, string text)
        {
            await context.FilmComments.AddAsync(new FilmComment()
            {
                FilmId = filmId,
                Text = text,
                UserId = userId
            });
            await context.SaveChangesAsync();

            return true;
        }

        public void ChangeTheme(HttpContext httpContext)
        {
            if (httpContext.Request.Cookies.ContainsKey("themeProject"))
            {
                string currTheme = httpContext.Request.Cookies["themeProject"];

                switch (currTheme)
                {
                    case "gray":
                        httpContext.Response.Cookies.Append("themeProject","white", cookieOpt);
                        break;
                    case "white":
                        httpContext.Response.Cookies.Append("themeProject","gray", cookieOpt);
                        break;
                }
            }
            else
            {
                httpContext.Response.Cookies.Append("themeProject", "white", cookieOpt);
            }
        }
    }
}
