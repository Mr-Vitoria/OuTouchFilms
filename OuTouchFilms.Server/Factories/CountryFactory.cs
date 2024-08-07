using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Factories
{
    public class CountryFactory
    {
        public Country GetDbFromView(CountryView objectView)
        {
            return new Country() { 
                Id = objectView.Id,
                Name = objectView.Name
            };
        }

        public async Task<CountryView> GetViewFromDb(Country objectDb)
        {
            return new CountryView(){ 
                Id = objectDb.Id,
                Name = objectDb.Name
            };
        }
    }
}
