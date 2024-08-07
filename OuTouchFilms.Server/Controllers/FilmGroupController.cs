using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OuTouchFilms.Server.Controllers.Models;
using OuTouchFilms.Server.Services;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilmGroupController : ControllerBase
    {
        private readonly IFilmGroupService animeGroupService;

        public FilmGroupController(IFilmGroupService _animeGroupService)
        {
            this.animeGroupService = _animeGroupService;
        }

        [HttpGet]
        public async Task<FilmGroupView> get(int id, bool needFull = false)
        {
            return await animeGroupService.GetFilmGroup(id, needFull);
        }

        [HttpGet]
        public async Task<FilmGroupMessage> getList(int count = 5, int page = 0, bool needFull = false)
        {
            Tuple<List<FilmGroupView>, bool> result = await animeGroupService.GetFilmGroupList(
                needFull: needFull,
                count: count,
                page: page
                );

            return new FilmGroupMessage()
            {
                GroupList = result.Item1,
                CanLoad = result.Item2,
                Page = page
            };
        }

    }
}
