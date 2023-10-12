using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OuTouchFilms.Services;
using OuTouchFilms.Models;
using System.Linq;
using System.Xml.Linq;

namespace OuTouchFilms.Controllers
{
    public class FilmsController : Controller
    {
        private readonly OuTouchDbContext context;
        private readonly IFilmService filmService;
        private readonly INewsService newsService;

        public FilmsController(OuTouchDbContext context, IFilmService filmService, INewsService newsService)
        {
            this.context = context;
            this.filmService = filmService;
            this.newsService = newsService;
        }


        public async Task<IActionResult> Index()
        {
            int userId = -1;
            int.TryParse(HttpContext.Request.Cookies["id"], out userId);

            //Проверка сервисной инфы
            await ServicesInfoService.AddCountFilmsVisit(context, HttpContext);

            return View(new
            {
                randomFilms = await filmService.getRandomFilms(10),
                lastFilms = await filmService.getLastFilmsByDate(10),
                news = await newsService.getLastNews(6),
                searchModel = await filmService.GetSearchModel(new string[0], new string[0]),
                userFilms = await filmService.GetLastUserFilms(6, userId)
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            //Проверка сервисной инфы
            await ServicesInfoService.AddCountFilmsVisit(context, HttpContext);


            int userId = -1;
            if (HttpContext.Request.Cookies.ContainsKey("id"))
            {
                userId = int.Parse(HttpContext.Request.Cookies["id"]);
            }
            return View(await filmService.getFilmInformation(id, userId));
        }

        public async Task<IActionResult> AddDetailsInformation(int id, string lastUrl = "/")
        {
            //Проверка сервисной инфы
            await ServicesInfoService.AddCountFilmsVisit(context, HttpContext);


            await filmService.AddFullFilmsInformation(id);

            return Redirect(lastUrl);
        }

        public async Task<IActionResult> Search(string title, int count = -1, bool isNeedAddFilms = false)
        {
            //Проверка сервисной инфы
            await ServicesInfoService.AddCountFilmsVisit(context, HttpContext);


            var films = await filmService.getFilmsByTitle(title, count);

            if (isNeedAddFilms)
            {
                await filmService.AddFilmsByTitle(title);

                return RedirectToAction("Search","Films",new
                {
                    title = title,
                    count = count
                });
            }

            return View(new
            {
                films = films,
                title = title
            });
        }


        public async Task<List<Film>> GetFilmsByTitle(string title, int count = -1)
        {
            var films = await filmService.getMinimalFilmsByTitle(title, count);
            
            return films;
        }
        public async Task<IActionResult> FilmList(string[] genres,string[] countries, int page = 1, string sortBy = "Name", int minYear = -1, int maxYear = -1)
        {
            //Проверка сервисной инфы
            await ServicesInfoService.AddCountFilmsVisit(context, HttpContext);


            var filmsModel = await filmService.getAllFilms(30, page, sortBy, genres, countries, minYear, maxYear);
            return View(new
            {
                filmsModel = filmsModel,
                searchModel = await filmService.GetSearchModel(genres, countries, sortBy, minYear, maxYear)
            });
        }
        public async Task<IActionResult> FilmListPagination(string genresJson, int page = 1, string sortBy = "Name", int minYear = -1, int maxYear = -1)
        {

            return RedirectToAction("FilmList", new
            {
                genres = JsonConvert.DeserializeObject<string[]>(genresJson) ?? new string[0],
                page = page,
                sortBy = sortBy,
                minYear = minYear,
                maxYear = maxYear
            });
        }

        public async Task<IActionResult> GetRandomFilm()
        {
            return RedirectToAction("Details", "Films", new
            {
                id = (await context.Films.OrderBy(r => Guid.NewGuid()).Take(1).FirstOrDefaultAsync()).Id
            });
        }


        public async Task<IActionResult> ChangeUsersFilm(int userId, int filmId, TypeOfUserFilm typeFilmUser, string lastUrl)
        {
            var userFilm = await context.UserFilms.FirstOrDefaultAsync(f => f.UserId == userId && f.FilmId == filmId);

            if (userFilm != null)
            {
                userFilm.TypeOfUserFilm = typeFilmUser;
                userFilm.AddedDate = DateOnly.FromDateTime(DateTime.Now);
                context.UserFilms.Update(userFilm);
            }
            else
            {
                await context.UserFilms.AddAsync(new UserFilms()
                {
                    FilmId = filmId,
                    UserId = userId,
                    TypeOfUserFilm = typeFilmUser,
                    AddedDate = DateOnly.FromDateTime(DateTime.Now)
                });
            }

            await context.SaveChangesAsync();
            return Redirect(lastUrl);

        }
    }
}
