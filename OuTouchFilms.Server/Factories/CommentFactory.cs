using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Factories
{
    public class CommentFactory
    {
        public CommentFactory()
        {
        }

        public Comment GetDbFromView(CommentView objectView)
        {
            return new Comment() { 
                Id = objectView.Id,
                UserId = objectView.UserId,
                FilmId = objectView.FilmId,
                Text = objectView.Text
            };
        }

        public async Task<CommentView> GetViewFromDb(Comment objectDb)
        {
            return new CommentView() {
                Id = objectDb.Id,
                UserId = objectDb.UserId,
                FilmId = objectDb.FilmId,
                Text = objectDb.Text
            };
        }
    }
}
