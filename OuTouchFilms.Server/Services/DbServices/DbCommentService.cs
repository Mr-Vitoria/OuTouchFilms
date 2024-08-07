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

        public async Task<Tuple<List<CommentView>, bool>> GetCommentListByAnimeId(
            int filmId, 
            Func<Comment, object>? orderBy = null,
            int count = 5,
            int page = 0
            )
        {
            List<Comment> dbList = await dbService.GetCommentList(where: (comment) => comment.FilmId == filmId, orderBy);
            int maxIndexPage = dbList.Count / count;
            if (dbList.Count % count == 0)
            {
                maxIndexPage--;
            }
            dbList = dbList.Skip(count * page).Take(count).ToList();

            List<CommentView> viewList = new List<CommentView>();

            for (int i = 0; i < dbList.Count; i++)
            {
                viewList.Add(await factory.GetViewFromDb(dbList[i]));
            }

            return new Tuple<List<CommentView>, bool>(viewList, maxIndexPage != page);
        }

        public async Task<Tuple<List<CommentView>, bool>> GetCommentListByUserId(
            int userId, 
            Func<Comment, object>? orderBy = null,
            int count = 5,
            int page = 0)
        {
            List<Comment> dbList = await dbService.GetCommentList(where: (comment) => comment.UserId == userId, orderBy);
            int maxIndexPage = dbList.Count / count;
            if (dbList.Count % count == 0)
            {
                maxIndexPage--;
            }
            dbList = dbList.Skip(count * page).Take(count).ToList();

            List<CommentView> viewList = new List<CommentView>();

            for (int i = 0; i < dbList.Count; i++)
            {
                viewList.Add(await factory.GetViewFromDb(dbList[i]));
            }

            return new Tuple<List<CommentView>, bool>(viewList, maxIndexPage != page);
        }
    }
}
