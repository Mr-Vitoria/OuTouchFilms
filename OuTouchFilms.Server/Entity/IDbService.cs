using OuTouchFilms.Server.Entity.Models;
using System.Xml.Linq;

namespace OuTouchFilms.Server.Entity
{
    public interface IDbService
    {
        //Film Table
        public Task<Film?> GetFilm(int id);
        public Task<List<Film>> GetFilmList(Func<Film, bool>? where = null, Func<Film, object>? orderBy = null);

        //Film group table
        public Task<FilmGroup> GetFilmGroup(int id);
        public Task<List<FilmGroup>> GetFilmGroupList(Func<FilmGroup, bool>? where = null, Func<FilmGroup, object>? orderBy = null);

        //Comment Table
        public Task<Comment?> GetComment(int id);
        public Task<List<Comment>> GetCommentList(Func<Comment, bool>? where = null, Func<Comment, object>? orderBy = null);
        public Task<bool> AddComment(Comment comment);

        //Country Table
        public Task<Country?> GetCountry(int id);
        public Task<List<Country>> GetCountryList(Func<Country, bool>? where = null, Func<Country, object>? orderBy = null);

        //FilmStaff Table
        public Task<FilmStaff?> GetFilmStaff(int id);
        public Task<List<FilmStaff>> GetFilmStaffList(Func<FilmStaff, bool>? where = null, Func<FilmStaff, object>? orderBy = null);

        //Staff Table
        public Task<Staff?> GetStaff(int id);
        public Task<List<Staff>> GetStaffList(Func<Staff, bool>? where = null, Func<Staff, object>? orderBy = null);

        //Translation Table
        public Task<Translation?> GetTranslation(int id);
        public Task<List<Translation>> GetTranslationList(Func<Translation, bool>? where = null, Func<Translation, object>? orderBy = null);

        //Genre Table
        public Task<Genre?> GetGenre(int id);
        public Task<List<Genre>> GetGenreList(Func<Genre, bool>? where = null, Func<Genre, object>? orderBy = null);

        //Studio Table
        public Task<Studio?> GetStudio(int id);
        public Task<List<Studio>> GetStudioList(Func<Studio, bool>? where = null, Func<Studio, object>? orderBy = null);

        //UserFilm Table
        public Task<UserFilm?> GetUsersFilm(int id);
        public Task<List<UserFilm>> GetUserFilmList(int userId, Func<UserFilm, bool>? where = null, Func<UserFilm, object>? orderBy = null, int count = -1);
        public Task<bool> AddUserFilm(UserFilm userFilm);
        public Task<bool> UpdateUserFilm(UserFilm userFilm, int id);
        public Task<bool> RemoveUserFilm(int id);
    }
}
