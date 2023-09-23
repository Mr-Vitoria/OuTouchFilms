using Microsoft.EntityFrameworkCore;

namespace OuTouchFilms.Models
{
    public class Film
    {
        public int Id { get; set; }
        public int KinopoiskId { get; set; }
        public string? ImdbId { get; set; }
        public float? KinopoiskRating { get; set; }
        public float? ImdbRating { get; set; }
        public string? Title { get; set; }
        public string? OriginalTitle { get; set; }
        public string? Poster { get; set; }
        public string? CoverPoster { get; set; }
        public int Year { get; set; }
        public string? Duration { get; set; }
        public string? Description { get; set; }
        public string? Annotation { get; set; }
        public string? Status { get; set; }
        public string Type { get; set; }
        public string? LastUpdate { get; set; }
        public string Countries { get; set; }
        public string Genres { get; set; }
        public string? Slogan { get; set; }

        public async Task<string[]> GetGenres(OuTouchDbContext context)
        {
            List<FilmGenre> allGenres = await context.FilmGenres.ToListAsync();
            string[] genresId = Genres.Split(";");
            string[] genres = new string[genresId.Length - 1];

            for (int i = 0; i < genres.Length; i++)
            {
                genres[i] = allGenres.FirstOrDefault(g => g.Id == int.Parse(genresId[i])).Title;
            }
            return genres;
        }
        public async Task<string[]> GetCountries(OuTouchDbContext context)
        {
            List<Country> allCountries = await context.Countries.ToListAsync();
            string[] countriesId = Countries.Split(";");
            string[] countries = new string[countriesId.Length - 1];

            for (int i = 0; i < countries.Length; i++)
            {
                countries[i] = allCountries.FirstOrDefault(g => g.Id == int.Parse(countriesId[i])).Name;
            }
            return countries;
        }
    }
}
