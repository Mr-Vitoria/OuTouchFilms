using Microsoft.EntityFrameworkCore.Query;
using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services;
using OuTouchFilms.Server.Services.Models;
using System.Text;

namespace OuTouchFilms.Server.Factories
{
    public class FilmFactory
    {
        private readonly IGenreService genreService;
        private readonly ICountryService countryService;
        private readonly IStaffService staffService;

        public FilmFactory(
            IGenreService genreService,
            ICountryService countryService,
            IStaffService staffService)
        {
            this.genreService = genreService;
            this.countryService = countryService;
            this.staffService = staffService;
        }

        public async Task<FilmView> GetViewFromDb(Film objectDb, bool needFull = false)
        {
            //Инициализация
            List<GenreView> genres = new List<GenreView>();
            List<CountryView> countries = new List<CountryView>();
            
            List<StaffView> actors = new List<StaffView>();
            List<StaffView> composers = new List<StaffView>();
            List<StaffView> designs = new List<StaffView>();
            List<StaffView> editors = new List<StaffView>();
            List<StaffView> operators = new List<StaffView>();
            List<StaffView> producers = new List<StaffView>();
            List<StaffView> writers = new List<StaffView>();
            List<StaffView> directors = new List<StaffView>();

            List<string> screenshots = new List<string>();

            //Заполнение массивов из других таблиц
            if (needFull)
            {
                foreach (string genreId in objectDb.Genres.Split(';'))
                {
                    int id = -1;
                    int.TryParse(genreId, out id);
                    if (id != -1)
                        genres.Add(await genreService.GetGenre(id));
                }
                foreach (string countryId in objectDb.Countries.Split(';'))
                {
                    int id;
                    int.TryParse(countryId, out id);
                    if (id != 0)
                        countries.Add(await countryService.GetCountry(id));
                }


                foreach (string actorId in objectDb.ActorIds.Split(';'))
                {
                    int id;
                    int.TryParse(actorId, out id);
                    if (id != 0)
                        actors.Add(await staffService.GetStaff(id));
                }
                foreach (string composerId in objectDb.ComposerIds.Split(';'))
                {
                    int id;
                    int.TryParse(composerId, out id);
                    if (id != 0)
                        composers.Add(await staffService.GetStaff(id));
                }
                foreach (string designId in objectDb.DesignIds.Split(';'))
                {
                    int id;
                    int.TryParse(designId, out id);
                    if (id != 0)
                        designs.Add(await staffService.GetStaff(id));
                }
                foreach (string editorId in objectDb.EditorIds.Split(';'))
                {
                    int id;
                    int.TryParse(editorId, out id);
                    if (id != 0)
                        editors.Add(await staffService.GetStaff(id));
                }
                foreach (string operatorId in objectDb.OperatorIds.Split(';'))
                {
                    int id;
                    int.TryParse(operatorId, out id);
                    if (id != 0)
                        operators.Add(await staffService.GetStaff(id));
                }
                foreach (string producerId in objectDb.ProducerIds.Split(';'))
                {
                    int id;
                    int.TryParse(producerId, out id);
                    if (id != 0)
                        producers.Add(await staffService.GetStaff(id));
                }
                foreach (string writerId in objectDb.WriterIds.Split(';'))
                {
                    int id;
                    int.TryParse(writerId, out id);
                    if (id != 0)
                        writers.Add(await staffService.GetStaff(id));
                }
                foreach (string directorId in objectDb.DirectorIds.Split(';'))
                {
                    int id;
                    int.TryParse(directorId, out id);
                    if (id != 0)
                        directors.Add(await staffService.GetStaff(id));
                }
            }

            foreach (string screenshot in objectDb.Screenshots.Split(';'))
            {
                if (screenshot.Trim() != "")
                    screenshots.Add(screenshot);
            }

            return new FilmView()
            {
                Id = objectDb.Id,

                OriginalTitle = objectDb.OriginalTitle,
                Slogan = objectDb.Slogan,
                Title = objectDb.Title,

                Description = objectDb.Description,
                Genres = genres,
                Countries = countries,
                Screenshots = screenshots,
                Status = objectDb.Status,
                Type = objectDb.Type,
                Year = objectDb.Year,
                Duration = objectDb.Duration,
                Annotation = objectDb.Annotation,

                Poster = objectDb.Poster,
                CoverPoster = objectDb.CoverPoster,
                
                ImdbId = objectDb.ImdbId,
                ImdbRating = objectDb.ImdbRating,
                KinopoiskId = objectDb.KinopoiskId,
                KinopoiskRating = objectDb.KinopoiskRating,
                
                LastUpdate = objectDb.LastUpdate,
                
                Actors = actors,
                Composers = composers,
                Designs = designs,
                Directors = directors,
                Editors = editors,
                Operators = operators,
                Producers = producers,
                Writers = writers
            };
        }
    }
}
