using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Controllers.Models
{
    public class CommentMessage
    {
        public List<CommentView> CommentList { get; set; }
        public int Page { get; set; }
        public bool CanLoad { get; set; }
    }
}
