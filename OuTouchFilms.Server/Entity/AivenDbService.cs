using Microsoft.EntityFrameworkCore;
using OuTouchFilms.Server.Entity.Models;
using System.Linq;
using System.Xml.Linq;

namespace OuTouchFilms.Server.Entity
{
    public class AivenDbService : IDbService
    {
        private Tuple<List<Film>, DateTime> localFilmList;
        public AivenDbService()
        {
            updateLocalFilmList();
        }


        private void updateLocalFilmList()
        {
            using (AivenDbContext context = new AivenDbContext())
            {
                localFilmList = new Tuple<List<Film>, DateTime>(context.Films.ToList(), DateTime.Now);
            }
        }

        public async Task<bool> AddComment(Comment comment)
        {
            using (AivenDbContext context = new AivenDbContext())
            {
                try
                {
                    await context.Comments.AddAsync(comment);
                    await context.SaveChangesAsync();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> AddUserFilm(UserFilm userFilm)
        {
            using (AivenDbContext context = new AivenDbContext())
            {
                try
                {
                    await context.UserFilms.AddAsync(userFilm);
                    await context.SaveChangesAsync();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<Film?> GetFilm(int id)
        {
            if ((DateTime.Now - localFilmList.Item2).TotalHours >= 4)
            {
                updateLocalFilmList();
            }

            return localFilmList.Item1.Find(an => an.Id == id);
        }
        public async Task<List<Film>> GetFilmList(Func<Film, bool>? where = null, Func<Film, object>? orderBy = null)
        {
            if ((DateTime.Now - localFilmList.Item2).TotalHours >= 4)
            {
                updateLocalFilmList();
            }

            List<Film> animeList;
            if (where != null)
                animeList = localFilmList.Item1.Where(where).ToList();
            else
                animeList = localFilmList.Item1;

            if (orderBy != null)
                return animeList.OrderBy(orderBy).ToList();

            return animeList;
        }

        public async Task<FilmGroup?> GetFilmGroup(int id)
        {
            using (AivenDbContext context = new AivenDbContext())
            {
                return await context.FilmGroups.FindAsync(id);
            }
        }

        public async Task<List<FilmGroup>> GetFilmGroupList(Func<FilmGroup, bool>? where = null, Func<FilmGroup, object>? orderBy = null)
        {
            List<FilmGroup> animeList;

            using (AivenDbContext context = new AivenDbContext())
            {
                if (where != null)
                    animeList = context.FilmGroups.Where(where).ToList();
                else
                    animeList = await context.FilmGroups.ToListAsync();
            }

            if (orderBy != null)
                return animeList.OrderBy(orderBy).ToList();

            return animeList;
        }

        public async Task<Comment?> GetComment(int id)
        {
            using (AivenDbContext context = new AivenDbContext())
            {
                return await context.Comments.FindAsync(id);
            }
        }

        public async Task<List<Comment>> GetCommentList(Func<Comment, bool>? where = null, Func<Comment, object>? orderBy = null)
        {
            List<Comment> commentList;

            using (AivenDbContext context = new AivenDbContext())
            {
                if (where != null)
                    commentList = context.Comments.Where(where).ToList();
                else
                    commentList = await context.Comments.ToListAsync();
            }

            if (orderBy != null)
                return commentList.OrderBy(orderBy).ToList();

            return commentList;
        }

        public async Task<Country?> GetCountry(int id)
        {
            using (AivenDbContext context = new AivenDbContext())
            {
                return await context.Countries.FindAsync(id);
            }
        }

        public async Task<List<Country>> GetCountryList(Func<Country, bool>? where = null, Func<Country, object>? orderBy = null)
        {
            List<Country> countryList;

            using (AivenDbContext context = new AivenDbContext())
            {
                if (where != null)
                    countryList = context.Countries.Where(where).ToList();
                else
                    countryList = await context.Countries.ToListAsync();
            }

            if (orderBy != null)
                return countryList.OrderBy(orderBy).ToList();

            return countryList;
        }

        public async Task<Genre?> GetGenre(int id)
        {
            using (AivenDbContext context = new AivenDbContext())
            {
                return await context.Genres.FindAsync(id);
            }
        }

        public async Task<List<Genre>> GetGenreList(Func<Genre, bool>? where = null, Func<Genre, object>? orderBy = null)
        {
            List<Genre> genreList;

            using (AivenDbContext context = new AivenDbContext())
            {
                if (where != null)
                    genreList = context.Genres.Where(where).ToList();
                else
                    genreList = await context.Genres.ToListAsync();
            }

            if (orderBy != null)
                return genreList.OrderBy(orderBy).ToList();

            return genreList;
        }

        public async Task<Studio?> GetStudio(int id)
        {
            using (AivenDbContext context = new AivenDbContext())
            {
                return await context.Studios.FindAsync(id);
            }
        }

        public async Task<List<Studio>> GetStudioList(Func<Studio, bool>? where = null, Func<Studio, object>? orderBy = null)
        {
            List<Studio> studioList;

            using (AivenDbContext context = new AivenDbContext())
            {
                if (where != null)
                    studioList = context.Studios.Where(where).ToList();
                else
                    studioList = await context.Studios.ToListAsync();
            }

            if (orderBy != null)
                return studioList.OrderBy(orderBy).ToList();

            return studioList;
        }

        public async Task<UserFilm?> GetUserFilm(int id)
        {
            using (AivenDbContext context = new AivenDbContext())
            {
                return await context.UserFilms.FindAsync(id);
            }
        }

        public async Task<List<UserFilm>> GetUserFilmList(
            int userId,
            Func<UserFilm, bool>? where = null,
            Func<UserFilm, object>? orderBy = null,
            int count = -1)
        {
            List<UserFilm> userFilmList;

            using (AivenDbContext context = new AivenDbContext())
            {
                if (where != null)
                    userFilmList = context.UserFilms.Where(usFilm => usFilm.UserId == userId).Where(where).ToList();
                else
                    userFilmList = await context.UserFilms.Where(usFilm => usFilm.UserId == userId).ToListAsync();
            }

            if (orderBy != null)
                return userFilmList.OrderBy(orderBy).Reverse().ToList();

            if (count != -1)
                userFilmList = userFilmList.Take(count).ToList();

            return userFilmList;
        }

        public async Task<bool> RemoveUserFilm(int id)
        {

            using (AivenDbContext context = new AivenDbContext())
            {
                UserFilm? removeUserFilm = await context.UserFilms.FindAsync(id);

                if (removeUserFilm == null)
                {
                    return false;
                }

                try
                {
                    context.UserFilms.Remove(removeUserFilm);
                    await context.SaveChangesAsync();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> UpdateUserFilm(UserFilm userFilm, int id)
        {
            using (AivenDbContext context = new AivenDbContext())
            {
                try
                {
                    userFilm.Id = id;
                    context.UserFilms.Update(userFilm);
                    await context.SaveChangesAsync();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<FilmStaff?> GetFilmStaff(int id)
        {
            using (AivenDbContext context = new AivenDbContext())
            {
                return await context.FilmStaffs.FindAsync(id);
            }
        }

        public async Task<List<FilmStaff>> GetFilmStaffList(Func<FilmStaff, bool>? where = null, Func<FilmStaff, object>? orderBy = null)
        {
            List<FilmStaff> filmStaffList;

            using (AivenDbContext context = new AivenDbContext())
            {
                if (where != null)
                    filmStaffList = context.FilmStaffs.Where(where).ToList();
                else
                    filmStaffList = await context.FilmStaffs.ToListAsync();
            }

            if (orderBy != null)
                return filmStaffList.OrderBy(orderBy).ToList();

            return filmStaffList;
        }

        public async Task<Staff?> GetStaff(int id)
        {
            using (AivenDbContext context = new AivenDbContext())
            {
                return await context.Staffs.FindAsync(id);
            }
        }

        public async Task<List<Staff>> GetStaffList(Func<Staff, bool>? where = null, Func<Staff, object>? orderBy = null)
        {
            List<Staff> StaffList;

            using (AivenDbContext context = new AivenDbContext())
            {
                if (where != null)
                    StaffList = context.Staffs.Where(where).ToList();
                else
                    StaffList = await context.Staffs.ToListAsync();
            }

            if (orderBy != null)
                return StaffList.OrderBy(orderBy).ToList();

            return StaffList;
        }

        public async Task<Translation?> GetTranslation(int id)
        {
            using (AivenDbContext context = new AivenDbContext())
            {
                return await context.Translations.FindAsync(id);
            }
        }

        public async Task<List<Translation>> GetTranslationList(Func<Translation, bool>? where = null, Func<Translation, object>? orderBy = null)
        {
            List<Translation> translationList;

            using (AivenDbContext context = new AivenDbContext())
            {
                if (where != null)
                    translationList = context.Translations.Where(where).ToList();
                else
                    translationList = await context.Translations.ToListAsync();
            }

            if (orderBy != null)
                return translationList.OrderBy(orderBy).ToList();

            return translationList;
        }

        public async Task<UserFilm?> GetUsersFilm(int id)
        {
            using (AivenDbContext context = new AivenDbContext())
            {
                return await context.UserFilms.FindAsync(id);
            }
        }
    }
}
