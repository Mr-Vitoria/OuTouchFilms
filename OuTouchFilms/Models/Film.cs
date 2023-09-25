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
        public string? Screenshots { get; set; }
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
        
        public string? EditorIds { get; set; }
        public string? DesignIds { get; set; }
        public string? ComposerIds { get; set; }
        public string? OperatorIds { get; set; }
        public string? WriterIds { get; set; }
        public string? ProducerIds { get; set; }
        public string? ActorIds { get; set; }
        public string? DirectorIds { get; set; }

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
            string[] countries = new string[countriesId.Length];

            for (int i = 0; i < countries.Length - 1; i++)
            {
                countries[i] = allCountries.FirstOrDefault(g => g.Id == int.Parse(countriesId[i])).Name;
            }
            return countries;
        }
        public async Task<Dictionary<string,List<FilmStaff>>> GetStaffs(OuTouchDbContext context)
        {
            Dictionary<string, List<FilmStaff>> staffs = new Dictionary<string, List<FilmStaff>>();

            await context.Staffs.LoadAsync();
            List<FilmStaff> filmStaffs = await context.FilmStaffs.Where(fs => fs.FilmId == Id).ToListAsync();

            staffs.Add("Editors", filmStaffs.Where(fs => fs.Profession == "Монтажеры").ToList());
            staffs.Add("Designs", filmStaffs.Where(fs => fs.Profession == "Художники").ToList());
            staffs.Add("Composers", filmStaffs.Where(fs => fs.Profession == "Композиторы").ToList());
            staffs.Add("Operators", filmStaffs.Where(fs => fs.Profession == "Операторы").ToList());
            staffs.Add("Writers", filmStaffs.Where(fs => fs.Profession == "Сценаристы").ToList());
            staffs.Add("Producers", filmStaffs.Where(fs => fs.Profession == "Продюсеры").ToList());
            staffs.Add("Actors", filmStaffs.Where(fs => fs.Profession == "Актеры").ToList());
            staffs.Add("Directors", filmStaffs.Where(fs => fs.Profession == "Режиссеры").ToList());

            return staffs;
        }
        public string[] GetScreenshots()
        {
            if(Screenshots != null)
            {
                return Screenshots.Split(';');
            }
            return null;
        }
    }
}
