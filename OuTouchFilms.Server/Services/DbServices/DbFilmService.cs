using Microsoft.AspNetCore.Mvc.RazorPages;
using OuTouchFilms.Server.Entity;
using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Factories;
using OuTouchFilms.Server.Services.Models;
using System.Globalization;
using System.Text;

namespace OuTouchFilms.Server.Services.DbServices
{
    public class DbFilmService : IFilmService
    {
        private readonly IDbService dbService;
        private readonly FilmFactory factory;
        private readonly Random random;

        public DbFilmService(IDbService dbService, FilmFactory factory)
        {
            this.dbService = dbService;
            this.factory = factory;
            this.random = new Random();
        }
        public async Task<FilmView> GetFilm(int id, bool needFull = false)
        {
            return await factory.GetViewFromDb(await dbService.GetFilm(id), needFull);
        }

        public async Task<Tuple<List<FilmView>, bool>> GetFilmList(
            Func<Film, object>? orderBy = null,
            Func<Film, bool>? where = null,
            bool needFull = false,
            int count = 5,
            int page = 0
        )
        {
            List<Film> dbList = await dbService.GetFilmList(where, orderBy);

            int maxIndexPage = page;
            if (count != -1)
            {
                maxIndexPage = dbList.Count / count;

                if (dbList.Count % count == 0)
                {
                    maxIndexPage--;
                }
                dbList = dbList.Skip(count * page).Take(count).ToList();
            }

            List<FilmView> viewList = new List<FilmView>();

            for (int i = 0; i < dbList.Count; i++)
            {
                viewList.Add(await factory.GetViewFromDb(dbList[i], needFull));
            }

            return new Tuple<List<FilmView>, bool>(viewList, maxIndexPage != page);
        }

        public async Task<Tuple<List<FilmView>, bool>> GetFilmListByTitle(
            string title,
            int count = 5,
            int page = 0
            )
        {
            title = getSearchString(title);

            return await GetFilmList(
                where: film => getSearchString(film.Title).Contains(title) || getSearchString(film.OriginalTitle).Contains(title),
                count: count,
                page: page
                );


            string getSearchString(string text)
            {
                text = text.Trim().Replace("ться", "тся").Replace("ё", "е").ToLower();
                char lastChar = '\0';

                StringBuilder sb = new StringBuilder();
                foreach (char c in text)
                {
                    if (c != lastChar)
                    {
                        lastChar = c;
                        sb.Append(c);
                    }
                }

                return sb.ToString();
            }

        }

        public async Task<int> GetRandomIndex()
        {
            IEnumerable<int> filmIdList = (await dbService.GetFilmList()).Select(an => an.Id);
            return filmIdList.ElementAt(random.Next(0, filmIdList.Count()));
        }
    }
}
