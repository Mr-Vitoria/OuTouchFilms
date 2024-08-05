using Microsoft.AspNetCore.Mvc;
//using OuTouchAnime.Server.Services;
//using OuTouchAnime.Server.Services.Models;

namespace OuTouchAnime.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        //private readonly ICommentService commentService;

        //public CommentController(ICommentService _commentService)
        //{
        //    this.commentService = _commentService;
        //}

        //[HttpGet]
        //public async Task<CommentView> get(int id)
        //{
        //    return await commentService.GetComment(id);
        //}

        //[HttpGet]
        //public async Task<List<CommentView>> getList(int animeId)
        //{
        //    return await commentService.GetCommentList(animeId);
        //}

        //[HttpGet]
        //public async Task<List<CommentView>> getListUser(int userId)
        //{
        //    return await commentService.GetUserCommentList(userId);
        //}

    }
}
