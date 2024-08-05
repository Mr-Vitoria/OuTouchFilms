using Microsoft.AspNetCore.Mvc;
using OuTouchFilms.Models;
using OuTouchFilms.Services;

namespace OuTouchFilms.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IMailService mailService;

        public UserController(IUserService userService, IMailService mailService)
        {
            this.userService = userService;
            this.mailService = mailService;
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

            await mailService.RegistrationLetter(user);
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
                ViewBag.Message = "Неправильно введен email, либо пароль";
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

        public async Task<IActionResult> ChangeUserProperty(string imgUrl, string emailSend)
        {
            await userService.ChangeCurrentUserProperties(HttpContext, imgUrl, emailSend);
            return RedirectToAction("Profile");

        }

        public async Task<IActionResult> AddComment(string text, int filmId, string lastUrl)
        {
            await userService.AddComment(int.Parse(HttpContext.Request.Cookies["id"]), filmId, text);

            return Redirect(lastUrl);
        }

        public IActionResult ChangeTheme(string lastUrl)
        {
            userService.ChangeTheme(HttpContext);

            return Redirect(lastUrl);
        }


        //Validations methods

        public async Task<bool> CheckLogin(string login)
        {
            return await userService.CheckLogin(login);
        }

        public async Task<bool> CheckEmail(string email)
        {
            return await userService.CheckEmail(email);
        }
    }
}
