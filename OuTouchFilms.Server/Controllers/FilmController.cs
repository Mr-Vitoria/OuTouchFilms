using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OuTouchFilms.Server.Controllers.Models;
using OuTouchFilms.Server.Services;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService filmService;
        private readonly IUserFilmService userFilmService;

        public FilmController(IFilmService _filmService, IUserFilmService _userFilmService)
        {
            this.filmService = _filmService;
            this.userFilmService = _userFilmService;
        }

        [HttpGet]
        public async Task<FilmView> get(int id, bool needFull = false, int userId = -1)
        {
            FilmView filmView = await filmService.GetFilm(id, needFull);

            if (userId != -1)
            {
                UserFilmView? userFilm = (await userFilmService.GetUserFilmList(
                                                                                    userId,
                                                                                    where: usFilm => usFilm.FilmId == filmView.Id
                                                                                    )
                                            ).Item1.FirstOrDefault();

                if (userFilm != null)
                {
                    filmView.isUserPlan = userFilm.TypeOfUserFilm == "PlanToWatch";
                }
            }
            return filmView;
        }

        [HttpGet]
        public async Task<FilmListMessage> getList(bool needFull = false, int count = -1, int page = 0)
        {
            Tuple<List<FilmView>, bool> result = await filmService.GetFilmList(
                needFull: needFull,
                count: count,
                page: page
                );

            return new FilmListMessage()
            {
                FilmList = result.Item1,
                CanLoad = result.Item2,
                Page = page
            };
        }

        //TODO: Реализовать логику случайных фильмов
        [HttpGet]
        public async Task<FilmListMessage> getRandomFilmList(bool needFull = false, int count = -1, int page = 0)
        {
            //Tuple<List<FilmView>, bool> result = await filmService.GetFilmList(
            //    needFull: needFull,
            //    count: count,
            //    page: page
            //    );

            //return new FilmListMessage()
            //{
            //    FilmList = result.Item1,
            //    CanLoad = result.Item2,
            //    Page = page
            //};

            return new FilmListMessage()
            {
                FilmList = new List<FilmView>(),//result.Item1,
                CanLoad = false, //result.Item2,
                Page = 0
            };
        }

        //TODO: Реализовать логику похожих фильмов
        [HttpGet]
        public async Task<FilmListMessage> getRelatedFilms(int filmId)
        {
            //Tuple<List<FilmView>, bool> result = await filmService.GetFilmList(
            //    needFull: needFull,
            //    where: film => film.Franchise == franchise,
            //    orderBy: film => film.AiredData,
            //    count: -1
            //    );

            return new FilmListMessage()
            {
                FilmList = new List<FilmView>(),//result.Item1,
                CanLoad = false, //result.Item2,
                Page = 0
            };
        }

        [HttpGet]
        public async Task<int> getRandomIndex()
        {
            return await filmService.GetRandomIndex();
        }

        [HttpGet]
        public async Task<FilmListMessage> search(string? title, int count = 5, int page = 0)
        {
            Tuple<List<FilmView>, bool> result = title != null
                ?
                await filmService.GetFilmListByTitle(title, count, page)
                :
                await filmService.GetFilmList(count: count, page: page);

            return new FilmListMessage()
            {
                FilmList = result.Item1,
                CanLoad = result.Item2,
                Page = page
            };
        }
    }
}
