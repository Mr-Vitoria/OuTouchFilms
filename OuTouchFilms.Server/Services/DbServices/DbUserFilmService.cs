using OuTouchFilms.Server.Entity;
using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Factories;
using OuTouchFilms.Server.Services.Models;
using System.Xml.Linq;

namespace OuTouchFilms.Server.Services.DbServices
{
    public class DbUserFilmService : IUserFilmService
    {
        private const string NONE_TYPE_FILM = "None";

        private readonly IDbService dbService;
        private readonly UserFilmFactory factory;

        public DbUserFilmService(IDbService dbService, UserFilmFactory factory)
        {
            this.dbService = dbService;
            this.factory = factory;
        }

        public async Task<bool> AddUserFilm(UserFilmView userFilm)
        {
            return await dbService.AddUserFilm(factory.GetDbFromView(userFilm));
        }

        public async Task<Tuple<List<UserFilmView>,bool>> GetUserFilmList(
            int userId,
            Func<UserFilm, bool>? where = null, 
            Func<UserFilm, object>? orderBy = null, 
            int count = 10,
            int page = 0
            
            )
        {
            List<UserFilm> dbList = await dbService.GetUserFilmList(userId, where, orderBy);
            
            int maxIndexPage = dbList.Count / count;
            if (dbList.Count % count == 0)
            {
                maxIndexPage--;
            }
            dbList = dbList.Skip(count * page).Take(count).ToList();

            List<UserFilmView> viewList = new List<UserFilmView>();

            for (int i = 0; i < dbList.Count; i++)
            {
                viewList.Add(await factory.GetViewFromDb(dbList[i]));
            }

            return new Tuple<List<UserFilmView>, bool>(viewList, maxIndexPage != page);
        }

        public async Task<bool> RemoveUserFilm(int id)
        {
            return await dbService.RemoveUserFilm(id);
        }

        public async Task<bool> UpdateUserFilm(int userId, int animeId, string type)
        {
            List<UserFilm> userFilmList = await dbService.GetUserFilmList(
                userId,
                userFilm => userFilm.FilmId == animeId);

            if (userFilmList.Count == 0)
            {
                return await AddUserFilm(new UserFilmView()
                {
                    FilmId = animeId,
                    TypeOfUserFilm = type,
                    UserId = userId,
                    AddedDate = DateTime.Now
                });
            }
            UserFilm usersFilm = userFilmList[0];

            if (type == NONE_TYPE_FILM)
            {
                return await dbService.RemoveUserFilm(usersFilm.Id);
            }

            if (!NotNeedUpdateType(type, usersFilm))
            {
                usersFilm.TypeOfUserFilm = type;
            }
            usersFilm.AddedDate = DateTime.Now;
            return await dbService.UpdateUserFilm(usersFilm, usersFilm.Id);

            static bool NotNeedUpdateType(string type, UserFilm usersFilm)
            {
                return (type == "Watching" && usersFilm.TypeOfUserFilm == "Completed");
            }
        }
    }
}
