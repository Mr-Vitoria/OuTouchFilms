using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Services
{
    public interface IFilmService
    {
        public Task<Tuple<List<FilmView>, bool>> GetFilmList(
            Func<Film, object>? orderBy = null, 
            Func<Film, bool>? where = null, 
            bool needFull = false, 
            int count = 5, 
            int page = 0
        );
        
        public Task<Tuple<List<FilmView>, bool>> GetFilmListByTitle(
            string title, 
            int count = 5, 
            int page = 0
        );

        public Task<FilmView> GetFilm(int id, bool needFull = false);
        
        public Task<int> GetRandomIndex();
    }
}
