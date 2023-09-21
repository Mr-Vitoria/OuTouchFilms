using Microsoft.AspNetCore.Mvc;
using OuTouchFilms.Models;
using OuTouchFilms.Services;

namespace OuTouchFilms.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [ActionName("Registration")]
        public IActionResult RegistrationPage()
        {
            return View("Registration", new
            {
                user = new User()
            });
        }
        [HttpPost]
        [ActionName("Registration")]
        public async Task<IActionResult> Registration(User? user)
        {
            if (!await userService.AddUser(user, HttpContext))
            {
                return View("Registration", new
                {
                    user = user
                });
            }

            return RedirectToAction("Index", "Films");
        }

        [HttpGet]
        [ActionName("Login")]
        public IActionResult LoginPage()
        {
            return View("Login", new
            {
                user = new User()
            });
        }
        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> Login(User user)
        {
            if (!await userService.CheckUser(user, HttpContext))
            {
                return View("Login", new
                {
                    user = user
                });
            }

            return RedirectToAction("Index", "Films");
        }


        [HttpGet]
        public async Task<IActionResult> Profile(TypeOfUserFilm TypeOfUserFilm = TypeOfUserFilm.Completed)
        {
            var currentUserInfo = await userService.GetCurrentUserInfo(HttpContext, TypeOfUserFilm);
            if (currentUserInfo != null)
            {
                return View(currentUserInfo);
            }


            return View("Login", new
            {
                user = new User()
            });

        }

        [HttpGet]
        public new IActionResult SignOut()
        {
            userService.SignOut(HttpContext);
            return RedirectToAction("Index", "Films");

        }

        public async Task<IActionResult> ChangeImage(string imgUrl)
        {
            await userService.ChangeCurrentUserImage(HttpContext, imgUrl);
            return RedirectToAction("Profile");

        }

        public async Task<IActionResult> AddComment(string text, int filmId, string lastUrl)
        {
            await userService.AddComment(int.Parse(HttpContext.Request.Cookies["id"]), filmId, text);

            return Redirect(lastUrl);
        }

        public async Task<IActionResult> AdminPanel()
        {
            dynamic user = await userService.GetCurrentUserInfo(HttpContext, null);
            if (user != null && user.user.GetAccountImportant() > 5)
            {
                return View(user);
            }
            return RedirectToAction("Index", "Films");
        }

        public IActionResult ChangeTheme(string lastUrl)
        {
            userService.ChangeTheme(HttpContext);

            return Redirect(lastUrl);
        }

        //public async Task<bool> AddUserAсhievment(int achievmentId, int userId)
        //{
        //    return await userService.AddUserAchievment(achievmentId, userId);
        //}

        //public async Task<IActionResult> AddAchievement(Achievement achievement, string lastUrl)
        //{
        //    await userService.AddAchievement(achievement);

        //    return Redirect(lastUrl);
        //}
    }
}
