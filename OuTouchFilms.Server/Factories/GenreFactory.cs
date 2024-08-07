using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Factories
{
    public class GenreFactory
    {
        public Genre GetDbFromView(GenreView objectView)
        {
            return new Genre() { 
                Id = objectView.Id,
                Title = objectView.Title
            };
        }

        public async Task<GenreView> GetViewFromDb(Genre objectDb)
        {
            return new GenreView()
            {
                Id = objectDb.Id,
                Title = objectDb.Title
            };
        }
    }
}
