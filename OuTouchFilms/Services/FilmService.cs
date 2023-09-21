using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OuTouchFilms.Models;
using System.Globalization;
using System.Net;

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

    }
}
