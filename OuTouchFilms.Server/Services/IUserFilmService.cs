using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Services
{
    public interface IUserFilmService
    {
        public Task<Tuple<List<UserFilmView>, bool>> GetUserFilmList(
            int userId, 
            Func<UserFilm, bool>? where = null, 
            Func<UserFilm, object>? orderBy = null, 
            int count = 10,
            int page = 0
            );
        public Task<bool> AddUserFilm(UserFilmView userFilm);
        public Task<bool> UpdateUserFilm(int userId, int filmId, string type);
        public Task<bool> RemoveUserFilm(int id);
    }
}
