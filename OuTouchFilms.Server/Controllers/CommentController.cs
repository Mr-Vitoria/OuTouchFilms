using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OuTouchFilms.Server.Controllers.Models;
using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService _commentService)
        {
            this.commentService = _commentService;
        }

        [HttpGet]
        public async Task<CommentView> get(int id)
        {
            return await commentService.GetComment(id);
        }

        [HttpGet]
        public async Task<CommentMessage> getList(int filmId, int count = 5, int page = 0)
        {
            Tuple<List<CommentView>, bool> result = await commentService.GetCommentListByAnimeId( 
                filmId,
                count: count,
                page: page
                );

            return new CommentMessage()
            {
                CommentList = new List<CommentView>(),
                CanLoad = result.Item2,
                Page = page
            };
        }

        [HttpGet]
        public async Task<CommentMessage> getUserList(int userId, int count = 5, int page = 0)
        {
            Tuple<List<CommentView>, bool> result = await commentService.GetCommentListByUserId(
                userId,
                count: count,
                page: page
                );

            return new CommentMessage()
            {
                CommentList = new List<CommentView>(),
                CanLoad = result.Item2,
                Page = page
            };
        }

    }
}
