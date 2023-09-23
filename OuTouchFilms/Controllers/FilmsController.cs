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
            return View(new
            {
                randomFilms = await filmService.getRandomFilms(10),
                lastFilms = await filmService.getLastFilmsByDate(10),
                news = await newsService.getLastNews(6),
                searchModel = await filmService.GetSearchModel(new string[0])
            });
        }

        /*Не использовать!!! Переделай. Нет проверки существования видео к фильму*/
        public async Task<IActionResult> AddRandomFilm(string lastUrl = "/")
        {
            var urlSwagger = "https://kinopoiskapiunofficial.tech/api/v2.2/films?order=RATING&type=ALL&ratingFrom=0&ratingTo=10&yearFrom=1000&yearTo=3000&page=1";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", "038f49e8-10f0-495e-a44d-845920b960d9");
            httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(urlSwagger);
            Stream responseStream = httpResponseMessage.Content.ReadAsStream();
            StreamReader reader = new StreamReader(responseStream);

            dynamic filmsArray = (dynamic)JsonConvert.DeserializeObject(reader.ReadToEnd());
            for (int i = 0; i < filmsArray.items.Count; i++)
            {
                dynamic filmJson = filmsArray.items[i];
                int kinopoiskId = filmJson.kinopoiskId;
                if (await context.Films.FirstOrDefaultAsync(f => f.KinopoiskId == kinopoiskId) != null)
                {
                    continue;
                }
                Film film = new Film();

                film.KinopoiskId = kinopoiskId;
                film.ImdbId = filmJson.imdbId;

                film.Title = filmJson.nameRu;
                film.OriginalTitle = filmJson.nameOriginal;

                List<Country> countries = await context.Countries.ToListAsync();
                for (int j = 0; j < filmJson.countries.Count; j++)
                {
                    string country = filmJson.countries[j].country;

                    Country filmCountry = countries.FirstOrDefault(c => c.Name == country);
                    if (filmCountry == null)
                    {
                        filmCountry = new Country();
                        filmCountry.Name = country;
                        await context.Countries.AddAsync(filmCountry);
                        await context.SaveChangesAsync();
                        countries.Add(filmCountry);
                    }

                    film.Countries += filmCountry.Id + ";";
                }


                List<FilmGenre> genres = await context.FilmGenres.ToListAsync();
                for(int j = 0; j < filmJson.genres.Count; j++)
                {
                    string genre = filmJson.genres[j].genre;
                    genre = genre.Substring(0,1).ToUpper() + genre.Substring(1);

                    FilmGenre filmGenre = genres.FirstOrDefault(g => g.Title == genre);
                    if (filmGenre == null)
                    {
                        filmGenre = new FilmGenre();
                        filmGenre.Title = genre;
                        await context.FilmGenres.AddAsync(filmGenre);
                        await context.SaveChangesAsync();
                        genres.Add(filmGenre);
                    }

                    film.Genres += filmGenre.Id+";";
                }

                film.KinopoiskRating = filmJson.ratingKinopoisk;
                film.ImdbRating = filmJson.ratingImdb;
                film.Type = filmJson.type;
                film.Year = filmJson.year;


                film.Poster = filmJson.posterUrl;

                await context.Films.AddAsync(film);
                await context.SaveChangesAsync();

            }


            return Redirect(lastUrl);
        }

        public async Task<IActionResult> Details(int id)
        {
            int userId = -1;
            if (HttpContext.Request.Cookies.ContainsKey("id"))
            {
                userId = int.Parse(HttpContext.Request.Cookies["id"]);
            }
            return View(await filmService.getFilmInformation(id, userId));
        }

        public async Task<IActionResult> AddDetailsInformation(int id, string lastUrl = "/")
        {
            await filmService.AddFullFilmsInformation(id);

            return Redirect(lastUrl);
        }

        public async Task<IActionResult> Search(string title, int count = -1, bool isNeedAddFilms = false)
        {
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
        public async Task<IActionResult> FilmList(string[] genres, int page = 1, string sortBy = "Name", int minYear = -1, int maxYear = -1)
        {
            var filmsModel = await filmService.getAllFilms(30, page, sortBy, genres, minYear, maxYear);
            return View(new
            {
                filmsModel = filmsModel,
                searchModel = await filmService.GetSearchModel(genres, sortBy, minYear, maxYear)
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
                context.UserFilms.Update(userFilm);
            }
            else
            {
                await context.UserFilms.AddAsync(new UserFilms()
                {
                    FilmId = filmId,
                    UserId = userId,
                    TypeOfUserFilm = typeFilmUser
                });
            }


            await context.SaveChangesAsync();
            return Redirect(lastUrl);

        }

        public async Task<bool> ChangeUsersAnimeFromJs(int userId, int filmId, TypeOfUserFilm typeFilmUser)
        {
            UserFilms userFilm = await context.UserFilms.FirstOrDefaultAsync(f => f.UserId == userId && f.FilmId == filmId);
            Film film = await context.Films.FindAsync(filmId);
            if (userFilm != null)
            {
                if (typeFilmUser == TypeOfUserFilm.None)
                {
                    context.UserFilms.Remove(userFilm);
                }
                if (userFilm.TypeOfUserFilm != typeFilmUser)
                {
                    if (userFilm.TypeOfUserFilm == TypeOfUserFilm.Completed && typeFilmUser == TypeOfUserFilm.Watching)
                    {
                        return false;
                    }

                    if (typeFilmUser == TypeOfUserFilm.Completed && film.Status == "Выходит")
                    {
                        return false;
                    }
                    userFilm.TypeOfUserFilm = typeFilmUser;
                    context.UserFilms.Update(userFilm);
                }
            }
            else
            {
                await context.UserFilms.AddAsync(new UserFilms()
                {
                    FilmId = filmId,
                    UserId = userId,
                    TypeOfUserFilm = typeFilmUser
                });
            }


            await context.SaveChangesAsync();
            return true;

        }


        public async Task<IActionResult> AddFilmByTitle(string title,string lastUrl = "/")
        {
            await filmService.AddFilmsByTitle(title);

            return Redirect(lastUrl);
        }
        public async Task<IActionResult> AddFilmById(int id,string lastUrl = "/")
        {
            await filmService.AddFilmsById(id);

            return Redirect(lastUrl);
        }
    }
}
