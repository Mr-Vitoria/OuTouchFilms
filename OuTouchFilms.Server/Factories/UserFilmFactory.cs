using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Factories
{
    public class UserFilmFactory
    {
        private readonly IFilmService filmService;

        public UserFilmFactory(IFilmService _filmService) {
            this.filmService = _filmService;
        }


        public UserFilm GetDbFromView(UserFilmView objectView)
        {
            return new UserFilm() {
                Id = objectView.Id,
                FilmId = objectView.FilmId,
                UserId = objectView.UserId,
                AddedDate = objectView.AddedDate,
                TypeOfUserFilm = objectView.TypeOfUserFilm,
            };
        }

        public async Task<UserFilmView> GetViewFromDb(UserFilm objectDb)
        {
            return new UserFilmView()
            {
                Id = objectDb.Id,
                FilmId = objectDb.FilmId,
                UserId = objectDb.UserId,
                AddedDate = objectDb.AddedDate,
                TypeOfUserFilm = objectDb.TypeOfUserFilm,
                Film = await filmService.GetFilm(objectDb.FilmId, false),
            };
        }
    }
}
