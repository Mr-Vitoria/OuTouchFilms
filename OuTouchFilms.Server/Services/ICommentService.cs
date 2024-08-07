using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Services
{
    public interface ICommentService
    {
        public Task<Tuple<List<CommentView>, bool>> GetCommentListByAnimeId(
            int filmId, 
            Func<Comment, object>? orderBy = null,
            int count = 5,
            int page = 0
            );
        public Task<Tuple<List<CommentView>, bool>> GetCommentListByUserId(
            int userId, 
            Func<Comment, object>? orderBy = null,
            int count = 5,
            int page = 0
            );
        public Task<CommentView> GetComment(int id);
    }
}
