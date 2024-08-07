using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Services
{
    public interface IFilmGroupService
    {
        public Task<FilmGroupView> GetFilmGroup(int id, bool needFull = false);
        public Task<Tuple<List<FilmGroupView>, bool>> GetFilmGroupList(
            Func<FilmGroup, object>? orderBy = null, 
            Func<FilmGroup, bool>? where = null, 
            bool needFull = false,
            int count = 5,
            int page = 0
            );
    }
}
