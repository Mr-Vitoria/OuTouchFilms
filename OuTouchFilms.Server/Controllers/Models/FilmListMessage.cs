using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Controllers.Models
{
    public class FilmListMessage
    {
        public List<FilmView> FilmList { get; set; }
        public int Page { get; set; }
        public bool CanLoad { get; set; }
    }
}
