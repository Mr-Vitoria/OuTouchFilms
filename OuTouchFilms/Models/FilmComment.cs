using System.Xml.Linq;

namespace OuTouchFilms.Models
{
    public class FilmComment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FilmId { get; set; }
        public string Text { get; set; }

        public Film Film { get; set; }
        public User User { get; set; }
    }
}
