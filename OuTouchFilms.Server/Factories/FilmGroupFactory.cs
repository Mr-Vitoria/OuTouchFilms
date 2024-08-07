using Microsoft.EntityFrameworkCore.Query;
using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services;
using OuTouchFilms.Server.Services.DbServices;
using OuTouchFilms.Server.Services.Models;
using System.Text;

namespace OuTouchFilms.Server.Factories
{
    public class FilmGroupFactory
    {
        private readonly IFilmService filmService;

        public FilmGroupFactory(
            IFilmService filmService
            )
        {
            this.filmService = filmService;
        }

        public async Task<FilmGroupView> GetViewFromDb(FilmGroup objectDb, bool needFull = false)
        {
            //Инициализация
            List<FilmView> films = new List<FilmView>();

            //Заполнение массивов из других таблиц
            foreach (string filmId in objectDb.FilmIds.Split(';'))
            {
                int id;
                int.TryParse(filmId, out id);
                if (id != 0)
                    films.Add(await filmService.GetFilm(id,needFull));
            }

            return new FilmGroupView()
            {
                Id = objectDb.Id,
                Title = objectDb.Title,
                Films = films
            };
        }
    }
}
