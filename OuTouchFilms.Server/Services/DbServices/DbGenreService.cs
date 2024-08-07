using OuTouchFilms.Server.Entity;
using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Factories;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Services.DbServices
{
    public class DbGenreService : IGenreService
    {
        private readonly IDbService dbService;
        private readonly GenreFactory factory;

        public DbGenreService(IDbService dbService, GenreFactory factory)
        {
            this.dbService = dbService;
            this.factory = factory;
        }
        public async Task<GenreView> GetGenre(int id)
        {
            return await factory.GetViewFromDb(await dbService.GetGenre(id));
        }

        public async Task<List<GenreView>> GetGenreList(Func<Genre, bool>? where = null, Func<Genre, object>? orderBy = null)
        {
            List<Genre> dbList = await dbService.GetGenreList(where, orderBy);
            List<GenreView> viewList = new List<GenreView>();

            for (int i = 0; i < dbList.Count; i++)
            {
                viewList.Add(await factory.GetViewFromDb(dbList[i]));
            }

            return viewList;
        }
    }
}
