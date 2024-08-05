using Microsoft.AspNetCore.Mvc;
//using OuTouchAnime.Server.Services.Models;
//using OuTouchAnime.Server.Services;
//using OuTouchAnime.Server.Controllers.Models;

namespace OuTouchAnime.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAnimeController : ControllerBase
    {
        //private readonly IUserAnimeService userAnimeService;

        //public UserAnimeController(IUserAnimeService _userAnimeService)
        //{
        //    this.userAnimeService = _userAnimeService;
        //}

        //[HttpGet]
        //public async Task<UserAnimeMessage> getList(int userId, string type = "all", int count = 10, int page = 0)
        //{
        //    Tuple<List<UserAnimeView>, bool> result = await userAnimeService.GetUserAnimeList(
        //        userId,
        //        orderBy: usAnime => usAnime.AddedDate,
        //        where: (type != "all" ? usAnime => usAnime.TypeOfUserAnime == type : null),
        //        count: count,
        //        page: page
        //        );
        //    return new UserAnimeMessage()
        //    {
        //        UserAnimeList = result.Item1,
        //        CanLoad = result.Item2,
        //        Page = page
        //    };
        //}

        //[HttpPost]
        //public async Task<bool> update(int animeId, int userId, string type = "all")
        //{
        //    return await userAnimeService.UpdateUserAnime(
        //        userId,
        //        animeId,
        //        type
        //        );
        //}
    }
}
