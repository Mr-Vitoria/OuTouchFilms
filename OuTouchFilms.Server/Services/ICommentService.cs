using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Services
{
    public interface ICommentService
    {
        public Task<List<CommentView>> GetCommentList(int animeId, Func<Comment, object>? orderBy = null);
        public Task<List<CommentView>> GetUserCommentList(int userId, Func<Comment, object>? orderBy = null);
        public Task<CommentView> GetComment(int id);
    }
}
