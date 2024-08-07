using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Services
{
    public interface ICountryService
    {
        public Task<List<CountryView>> GetCountryList(Func<Country, bool>? where = null, Func<Country, object>? orderBy = null);
        public Task<CountryView> GetCountry(int id);
    }
}
