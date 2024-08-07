using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Controllers.Models
{
    public class FilmGroupMessage
    {
        public List<FilmGroupView> GroupList { get; set; }
        public int Page { get; set; }
        public bool CanLoad { get; set; }
    }
}
