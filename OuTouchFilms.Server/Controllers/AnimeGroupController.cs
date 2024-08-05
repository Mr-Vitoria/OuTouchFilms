using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using OuTouchAnime.Server.Controllers.Models;
//using OuTouchAnime.Server.Services;
//using OuTouchAnime.Server.Services.Models;

namespace OuTouchAnime.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnimeGroupController : ControllerBase
    {
        //private readonly IAnimeGroupService animeGroupService;

        //public AnimeGroupController(IAnimeGroupService _animeGroupService)
        //{
        //    this.animeGroupService = _animeGroupService;
        //}

        //[HttpGet]
        //public async Task<AnimeGroupView> get(int id, bool needFull = false)
        //{
        //    return await animeGroupService.GetAnimeGroup(id, needFull);
        //}

        //[HttpGet]
        //public async Task<AnimeGroupMessage> getList(int count = 5, int page = 0,bool needFull = false)
        //{
        //    Tuple<List<AnimeGroupView>, bool> result = await animeGroupService.GetAnimeGroupList(
        //        needFull: needFull,
        //        count: count,
        //        page: page
        //        );
            
        //    return new AnimeGroupMessage() { 
        //        GroupList = result.Item1,
        //        CanLoad = result.Item2,
        //        Page = page
        //    };
        //}

    }
}
