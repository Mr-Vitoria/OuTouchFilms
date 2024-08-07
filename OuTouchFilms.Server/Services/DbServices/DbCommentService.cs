using OuTouchFilms.Server.Entity;
using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Factories;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Services.DbServices
{
    public class DbCommentService : ICommentService
    {
        private readonly IDbService dbService;
        private readonly CommentFactory factory;

        public DbCommentService(IDbService dbService, CommentFactory factory)
        {
            this.dbService = dbService;
            this.factory = factory;
        }

        public async Task<CommentView> GetComment(int id)
        {
            return await factory.GetViewFromDb(await dbService.GetComment(id));
        }

        public async Task<List<CommentView>> GetCommentList(int filmId, Func<Comment, object>? orderBy = null)
        {
            List<Comment> dbList = await dbService.GetCommentList(where: (comment) => comment.FilmId == filmId, orderBy);
            List<CommentView> viewList = new List<CommentView>();

            for (int i = 0; i < dbList.Count; i++)
            {
                viewList.Add(await factory.GetViewFromDb(dbList[i]));
            }

            return viewList;
        }

        public async Task<List<CommentView>> GetUserCommentList(int userId, Func<Comment, object>? orderBy = null)
        {
            List<Comment> dbList = await dbService.GetCommentList(where: (comment) => comment.UserId == userId, orderBy);
            List<CommentView> viewList = new List<CommentView>();

            for (int i = 0; i < dbList.Count; i++)
            {
                viewList.Add(await factory.GetViewFromDb(dbList[i]));
            }

            return viewList;
        }
    }
}
