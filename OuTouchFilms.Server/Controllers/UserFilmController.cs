using Microsoft.AspNetCore.Mvc;
using OuTouchFilms.Server.Services.Models;
using OuTouchFilms.Server.Services;
using OuTouchFilms.Server.Controllers.Models;

namespace OuTouchFilms.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserFilmController : ControllerBase
    {
        private readonly IUserFilmService userFilmService;

        public UserFilmController(IUserFilmService _userFilmService)
        {
            this.userFilmService = _userFilmService;
        }

        [HttpGet]
        public async Task<UserFilmMessage> getList(int userId, string type = "all", int count = 10, int page = 0)
        {
            Tuple<List<UserFilmView>, bool> result = await userFilmService.GetUserFilmList(
                userId,
                orderBy: usFilm => usFilm.AddedDate,
                where: (type != "all" ? usFilm => usFilm.TypeOfUserFilm == type : null),
                count: count,
                page: page
                );
            return new UserFilmMessage()
            {
                UserFilmList = result.Item1,
                CanLoad = result.Item2,
                Page = page
            };
        }

        [HttpPost]
        public async Task<bool> update(int animeId, int userId, string type = "all")
        {
            return await userFilmService.UpdateUserFilm(
                userId,
                animeId,
                type
                );
        }
    }
}
