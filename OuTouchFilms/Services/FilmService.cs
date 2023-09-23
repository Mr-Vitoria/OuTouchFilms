using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OuTouchFilms.Models;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Reflection.PortableExecutable;

namespace OuTouchFilms.Services
{
    public class FilmService : IFilmService
    {
        private readonly OuTouchDbContext context;

        public FilmService(OuTouchDbContext context)
        {
            this.context = context;
        }


        public async Task<object> GetSearchModel(string[] currentGenres, string sortBy = "Name", int currMinYear = -1, int currMaxYear = -1)
        {
            int minYear = await context.Films.Select(f => f.Year).MinAsync();
            int maxYear = await context.Films.Select(f => f.Year).MaxAsync();
            return new
            {
                allGenres = await context.FilmGenres.ToListAsync(),
                minYear = minYear,
                maxYear = maxYear,
                sortBy = sortBy,
                currentGenres = currentGenres,
                currentMinYear = (currMinYear == -1 ? minYear : currMinYear),
                currentMaxYera = (currMaxYear == -1 ? maxYear : currMaxYear)
            };
        }

        public async Task<object> getRandomFilms(int count)
        {

            var films = new List<object>();
            var randomFilms = await context.Films
                                                        .OrderBy(r => Guid.NewGuid())
                                                        .Take(count)
                                                        .ToListAsync();


            for (int i = 0; i < randomFilms.Count(); i++)
            {
                films.Add(new
                {
                    film = randomFilms[i],
                    genres = await randomFilms[i].GetGenres(context)
                });

            }
            return films;
        }
        public async Task<object> getLastFilmsById(int count)
        {
            var films = new List<object>();
            var lastFilms = await context.Films.OrderByDescending(f => f.Id).Take(count).ToListAsync();


            for (int i = 0; i < lastFilms.Count(); i++)
            {
                films.Add(new
                {
                    Film = lastFilms.ElementAt(i),
                    genres = await lastFilms.ElementAt(i).GetGenres(context)
                }) ;

            }
            return films;
        }
        public async Task<object> getLastFilmsByDate(int count)
        {
            var lastFilms = await context.Films.OrderByDescending(f => f.Year).Take(count).ToListAsync();
            return lastFilms;
        }

        public async Task<List<object>> getFilmsByTitle(string title, int count)
        {
            var findFilms = await context.Films.Where(f => EF.Functions.Like(f.Title
                                                                                    .ToLower()
                                                                                    .Replace(",", "")
                                                                                    .Replace(".", "")
                                                                                    .Replace(" ", "")
                                                                                    .Replace("-", "")
                                                                                    .Replace(":", "")
                                                                                    .Replace("нн", "н")
                                                                                    .Replace("ё", "е"), $"%{title.ToLower()
                                                                                                                .Replace(",", "")
                                                                                                                .Replace(".", "")
                                                                                                                .Replace(" ", "")
                                                                                                                .Replace(":", "")
                                                                                                                .Replace("-", "")
                                                                                    .Replace("нн", "н")
                                                                                                                .Replace("ё", "е")}%")
                                                                                || 
                                                                                EF.Functions.Like(f.OriginalTitle
                                                                                .ToLower()
                                                                                .Replace(",", "")
                                                                                .Replace(".", "")
                                                                                .Replace(" ", "")
                                                                                .Replace("-", "")
                                                                                .Replace(":", "")
                                                                                .Replace("нн", "н")
                                                                                .Replace("ё", "е"), $"%{title.ToLower()
                                                                                                            .Replace(",", "")
                                                                                                            .Replace(".", "")
                                                                                                            .Replace(" ", "")
                                                                                                            .Replace(":", "")
                                                                                                            .Replace("-", "")
                                                                                                            .Replace("нн", "н")
                                                                                                            .Replace("ё", "е")}%"))
                                                                            .OrderBy(f => f.Title) 
                                                                            .ToListAsync();
            var films = new List<object>();

            if (count < 0)
            {
                count = findFilms.Count();
            }
            for (int i = 0; i < findFilms.Count() && i < count; i++)
            {
                films.Add(new
                {
                    film = findFilms.ElementAt(i),
                    genres = await findFilms.ElementAt(i).GetGenres(context)
                });

            }


            return films;
        }
        public async Task<List<Film>> getMinimalFilmsByTitle(string title, int count)
        {
            var findFilms = await context.Films.Where(f => EF.Functions.Like(f.Title
                                                                                    .ToLower()
                                                                                    .Replace(",", "")
                                                                                    .Replace(".", "")
                                                                                    .Replace(" ", "")
                                                                                    .Replace("-", "")
                                                                                    .Replace(":", "")
                                                                                    .Replace("нн", "н")
                                                                                    .Replace("ё", "е"), $"%{title.ToLower()
                                                                                                                .Replace(",", "")
                                                                                                                .Replace(".", "")
                                                                                                                .Replace(" ", "")
                                                                                                                .Replace(":", "")
                                                                                                                .Replace("-", "")
                                                                                    .Replace("нн", "н")
                                                                                                                .Replace("ё", "е")}%")
                                                                                || 
                                                                                EF.Functions.Like(f.OriginalTitle
                                                                                .ToLower()
                                                                                .Replace(",", "")
                                                                                .Replace(".", "")
                                                                                .Replace(" ", "")
                                                                                .Replace("-", "")
                                                                                .Replace(":", "")
                                                                                .Replace("нн", "н")
                                                                                .Replace("ё", "е"), $"%{title.ToLower()
                                                                                                            .Replace(",", "")
                                                                                                            .Replace(".", "")
                                                                                                            .Replace(" ", "")
                                                                                                            .Replace(":", "")
                                                                                                            .Replace("-", "")
                                                                                                            .Replace("нн", "н")
                                                                                                            .Replace("ё", "е")}%"))
                                                                            .OrderBy(f => f.Title) 
                                                                            .ToListAsync();

            if (count > 0)
            {
                findFilms = findFilms.Take(count).ToList();
            }

            return findFilms;
        }
        public async Task<object> getAllFilms(int count, int page, string sortBy, string[] genres, int minYear, int maxYear)
        {
            if (maxYear == -1)
                maxYear = DateTime.Now.Year;

            var films = new List<object>();
            List<Film> allFilms = await GetFilmBySort(genres,minYear,maxYear);

            int allFilmsCount = allFilms.Count;

            switch (sortBy)
            {
                case "Name":
                    allFilms = allFilms.OrderBy(f => f.Title).Skip((page - 1) * count).Take(count).ToList();
                    break;
                case "Date":
                    allFilms = allFilms.OrderBy(f => f.Year).Skip((page - 1) * count).Take(count).ToList();
                    break;
                case "Score":
                    allFilms = allFilms.OrderBy(f => f.KinopoiskRating).Skip((page - 1) * count).Take(count).ToList();
                    break;
                case "NameReverse":
                    allFilms = allFilms.OrderByDescending(f => f.Title).Skip((page - 1) * count).Take(count).ToList();
                    break;
                case "DateReverse":
                    allFilms = allFilms.OrderByDescending(f => f.Year).Skip((page - 1) * count).Take(count).ToList();
                    break;
                case "ScoreReverse":
                    allFilms = allFilms.OrderByDescending(f => f.KinopoiskRating).Skip((page - 1) * count).Take(count).ToList();
                    break;
                default:
                    allFilms = allFilms.Skip(page * count).Take(count).ToList();
                    break;
            }


            for (int i = 0; i < allFilms.Count; i++)
            {
                films.Add(new
                {
                    film = allFilms[i]
                });

            }
            return new
            {
                films = films,
                page = page,
                maxPage = (allFilmsCount % count == 0 ? allFilmsCount / count : (allFilmsCount / count) + 1),
                sortBy = sortBy
            };
        }

        public async Task<List<Film>> GetFilmBySort(string[] genres, int minYear, int maxYear)
        {
            var films = await context.Films.ToListAsync();
            if (genres.Count() > 0)
            {
                films = films.Where(f => genres
                                                .All(g => f.Genres
                                                                .Split(';', StringSplitOptions.None)
                                                                .Contains(g)) == true)
                                .ToList();
            }

            return films.Where(f => f.Year >= minYear)
                         .Where(f => f.Year <= maxYear)
                         .ToList();
        }

        public async Task<object> getFilmInformation(int filmId,int userId = -1)
        {

            var filmDb = await context.Films.FindAsync(filmId);
            List<Film> franchiseFilms = null;
            await context.Users.LoadAsync();

            TypeOfUserFilm type = TypeOfUserFilm.None;
            int accountImportant = 0;
            if(userId != -1)
            {
                UserFilms usersFilm = await context.UserFilms.FirstOrDefaultAsync(f => f.FilmId == filmId && f.UserId == userId);
                if(usersFilm != null)
                {
                    type = usersFilm.TypeOfUserFilm;
                }
                User user = await context.Users.FindAsync(userId);
                if (user != null)
                {
                    accountImportant = user.GetAccountImportant();
                }
            }

            return new
            {
                film = filmDb,
                genres = await filmDb.GetGenres(context),
                filmComments = await context.FilmComments.Where(comm => comm.FilmId == filmDb.Id).ToListAsync(),
                userType = type,
                accountImportant = accountImportant
            };
        }


        public async Task<bool> AddFilmsByTitle(string title)
        {
            var urlSwagger = "https://kinopoiskapiunofficial.tech/api/v2.1/films/search-by-keyword?keyword=" + title + "&page=1";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", "038f49e8-10f0-495e-a44d-845920b960d9");
            httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(urlSwagger);
            Stream responseStream = httpResponseMessage.Content.ReadAsStream();
            StreamReader reader = new StreamReader(responseStream);

            dynamic filmsArray = (dynamic)JsonConvert.DeserializeObject(reader.ReadToEnd());
            for (int i = 0; i < filmsArray.films.Count; i++)
            {
                dynamic filmJson = filmsArray.films[i];

                var urlVideo = "https://kinobox.tv/api/players/main?kinopoisk=" + filmJson.filmId;
                HttpClient httpClientVideo = new HttpClient();
                httpClientVideo.DefaultRequestHeaders.Add("accept", "application/json");
                HttpResponseMessage httpResponseMessageVideo = await httpClientVideo.GetAsync(urlVideo);
                StreamReader readerVideo = new StreamReader(httpResponseMessageVideo.Content.ReadAsStream());

                dynamic filmsVideo = (dynamic)JsonConvert.DeserializeObject(readerVideo.ReadToEnd());
                if (filmsVideo.Count <= 0)
                {
                    continue;
                }
                await AddFilmsById((int)filmJson.filmId);
            }

            return true;
        }        
        
        public async Task<bool> AddFilmsById(int id)
        {
            var urlSwagger = "https://kinopoiskapiunofficial.tech/api/v2.2/films/" + id;
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", "038f49e8-10f0-495e-a44d-845920b960d9");
            httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(urlSwagger);
            Stream responseStream = httpResponseMessage.Content.ReadAsStream();
            StreamReader reader = new StreamReader(responseStream);

            dynamic filmJson = (dynamic)JsonConvert.DeserializeObject(reader.ReadToEnd());
            
            return await AddFilmsByJson(filmJson);
        }

        public async Task<bool> AddFilmsByJson(dynamic filmJson, bool isNeedCheck = true)
        {
            int kinopoiskId = filmJson.kinopoiskId;

            if (await context.Films.FirstOrDefaultAsync(f => f.KinopoiskId == kinopoiskId) != null)
            {
                return false;
            }

            if (isNeedCheck)
            {
                var urlVideo = "https://kinobox.tv/api/players/main?kinopoisk=" + kinopoiskId;
                HttpClient httpClientVideo = new HttpClient();
                httpClientVideo.DefaultRequestHeaders.Add("accept", "application/json");
                HttpResponseMessage httpResponseMessageVideo = await httpClientVideo.GetAsync(urlVideo);
                StreamReader readerVideo = new StreamReader(httpResponseMessageVideo.Content.ReadAsStream());

                dynamic filmsVideo = (dynamic)JsonConvert.DeserializeObject(readerVideo.ReadToEnd());
                if (filmsVideo.Count<=0)
                {
                    return false;
                }
            }
            Film film = new Film();

            film.KinopoiskId = kinopoiskId;

            film.Title = filmJson.nameRu;
            film.OriginalTitle = filmJson.nameOriginal == null ? filmJson.nameOriginal : filmJson.nameEn;
            film.Description = filmJson.description;


            List<Country> countries = await context.Countries.ToListAsync();
            for (int j = 0; j < filmJson.countries.Count; j++)
            {
                string country = filmJson.countries[j].country;

                Country filmCountry = countries.FirstOrDefault(c => c.Name == country);
                if (filmCountry == null)
                {
                    filmCountry = new Country();
                    filmCountry.Name = country;
                    await context.Countries.AddAsync(filmCountry);
                    await context.SaveChangesAsync();
                    countries.Add(filmCountry);
                }

                film.Countries += filmCountry.Id + ";";
            }


            List<FilmGenre> genres = await context.FilmGenres.ToListAsync();
            for (int j = 0; j < filmJson.genres.Count; j++)
            {
                string genre = filmJson.genres[j].genre;
                genre = genre.Substring(0, 1).ToUpper() + genre.Substring(1);

                FilmGenre filmGenre = genres.FirstOrDefault(g => g.Title == genre);
                if (filmGenre == null)
                {
                    filmGenre = new FilmGenre();
                    filmGenre.Title = genre;
                    await context.FilmGenres.AddAsync(filmGenre);
                    await context.SaveChangesAsync();
                    genres.Add(filmGenre);
                }

                film.Genres += filmGenre.Id + ";";
            }


            float rating = 0.0f;
            string ratingString = filmJson.ratingKinopoisk + "";
            float.TryParse(ratingString, out rating);

            film.KinopoiskRating = rating;
            string type = filmJson.type;

            switch (type)
            {
                case "TV_SERIES":
                    type = "Сериал";
                    break;
                case "FILM":
                    type = "Фильм";
                    break;
                case "VIDEO":
                    type = "Видео";
                    break;
                case "MINI_SERIES":
                    type = "Мини сериал";
                    break;
                default:
                    break;
            }
            film.Type = type;
            int year = 0;
            string yearString = filmJson.year + "";
            int.TryParse(yearString, out year);
            film.Year =year;

            film.Poster = filmJson.posterUrl;
            film.Duration = filmJson.filmLength;

            film.Slogan = filmJson.slogan;
            film.Annotation = filmJson.editorAnnotation;
            film.LastUpdate = filmJson.lastSync;



            await context.Films.AddAsync(film);
            await context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> AddFullFilmsInformation(int id)
        {
            Film film = await context.Films.FindAsync(id);

            if(film == null)
            {
                return false;
            }

            var staffInformationUrl = "https://kinopoiskapiunofficial.tech/api/v1/staff?filmId=" + film.KinopoiskId;
            HttpClient httpClientstaffInformation = new HttpClient();
            httpClientstaffInformation.DefaultRequestHeaders.Add("accept", "application/json");
            httpClientstaffInformation.DefaultRequestHeaders.Add("X-API-KEY", "038f49e8-10f0-495e-a44d-845920b960d9");
            HttpResponseMessage httpResponseMessagestaffInformation = await httpClientstaffInformation.GetAsync(staffInformationUrl);
            StreamReader readerstaffInformation = new StreamReader(httpResponseMessagestaffInformation.Content.ReadAsStream());
            dynamic staffArray = (dynamic)JsonConvert.DeserializeObject(readerstaffInformation.ReadToEnd());

            film.EditorIds = "";
            film.DesignIds = "";
            film.ComposerIds = "";
            film.OperatorIds = "";
            film.WriterIds = "";
            film.ProducerIds = "";
            film.ActorIds = "";
            for (int i = 0; i < staffArray.Count; i++)
            {
                int staffId = staffArray[i].staffId;
                Staff staff = await context.Staffs.FirstOrDefaultAsync(st => st.SwaggerId == staffId);
                if(staff == null)
                {
                    staff = new Staff()
                    {
                        Name = staffArray[i].nameRu,
                        Poster = staffArray[i].posterUrl,
                        SwaggerId = staffId
                    };

                    await context.Staffs.AddAsync(staff);
                    await context.SaveChangesAsync();
                }

                FilmStaff filmStaff = await context.FilmStaffs.FirstOrDefaultAsync(fs => fs.StaffId == staff.Id && fs.FilmId == film.Id);

                if(filmStaff == null)
                {
                    string description = staffArray[i].description;
                    
                    if(description!=null)
                        description = description.Substring(0, 1).ToUpper() + description.Substring(1);
                    
                    
                    filmStaff = new FilmStaff()
                    {
                        Profession = staffArray[i].professionText,
                        Description = description,
                        FilmId = film.Id,
                        StaffId = staff.Id
                    };

                    await context.FilmStaffs.AddAsync(filmStaff);
                    await context.SaveChangesAsync();
                }

                switch (filmStaff.Profession)
                {
                    case "Режиссеры":
                        film.DirectorIds += filmStaff.Id + ";";
                        break;
                    case "Актеры":
                        film.ActorIds += filmStaff.Id + ";";
                        break;
                    case "Монтажеры":
                        film.EditorIds += filmStaff.Id + ";";
                        break;
                    case "Художники":
                        film.DesignIds += filmStaff.Id + ";";
                        break;
                    case "Композиторы":
                        film.ComposerIds += filmStaff.Id + ";";
                        break;
                    case "Операторы":
                        film.OperatorIds += filmStaff.Id + ";";
                        break;
                    case "Сценаристы":
                        film.WriterIds += filmStaff.Id + ";";
                        break;
                    case "Продюсеры":
                        film.ProducerIds += filmStaff.Id + ";";
                        break;
                    default:
                        break;
                }

            }

            context.Films.Update(film);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
