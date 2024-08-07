using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Services
{
    public interface IGenreService
    {
        public Task<List<GenreView>> GetGenreList(Func<Genre, bool>? where = null, Func<Genre, object>? orderBy = null);
        public Task<GenreView> GetGenre(int id);
    }
}
