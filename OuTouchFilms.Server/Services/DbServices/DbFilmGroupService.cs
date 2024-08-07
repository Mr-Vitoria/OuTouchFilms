using OuTouchFilms.Server.Entity;
using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Factories;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Services.DbServices
{
    public class DbFilmGroupService : IFilmGroupService
    {
        private readonly IDbService dbService;
        private readonly FilmGroupFactory factory;

        public DbFilmGroupService(IDbService dbService, FilmGroupFactory factory)
        {
            this.dbService = dbService;
            this.factory = factory;
        }
        public async Task<FilmGroupView> GetFilmGroup(int id, bool needFull = false)
        {
            return await factory.GetViewFromDb(await dbService.GetFilmGroup(id), needFull);
        }

        public async Task<Tuple<List<FilmGroupView>, bool>> GetFilmGroupList(
            Func<FilmGroup, object>? orderBy = null, 
            Func<FilmGroup, bool>? where = null, 
            bool needFull = false,
            int count = 5,
            int page = 0
            )
        {
            List<FilmGroup> dbList = await dbService.GetFilmGroupList(where, orderBy);
            int maxIndexPage = dbList.Count / count;
            if(dbList.Count % count == 0)
            {
                maxIndexPage--;
            }
            dbList = dbList.Skip(count * page).Take(count).ToList();
            
            List<FilmGroupView> viewList = new List<FilmGroupView>();

            for (int i = 0; i < dbList.Count; i++)
            {
                viewList.Add(await factory.GetViewFromDb(dbList[i], needFull));
            }

            return new Tuple<List<FilmGroupView>, bool>(viewList, maxIndexPage != page);
        }
    }
}
