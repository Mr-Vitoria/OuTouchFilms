using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Controllers.Models
{
    public class UserFilmMessage
    {
        public List<UserFilmView> UserFilmList { get; set; }
        public int Page { get; set; }
        public bool CanLoad { get; set; }
    }
}
