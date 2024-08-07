using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Factories
{
    public class StaffFactory
    {
        public Staff GetDbFromView(StaffView objectView)
        {
            return new Staff() { 
                Id = objectView.Id,
                Name = objectView.Name,
                Poster = objectView.Poster,
                SwaggerId = objectView.SwaggerId
            };
        }

        public async Task<StaffView> GetViewFromDb(Staff objectDb)
        {
            return new StaffView()
            {
                Id = objectDb.Id,
                Name = objectDb.Name,
                Poster = objectDb.Poster,
                SwaggerId = objectDb.SwaggerId
            };
        }
    }
}
