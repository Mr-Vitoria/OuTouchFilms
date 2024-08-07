using OuTouchFilms.Server.Entity;
using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Factories;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Services.DbServices
{
    public class DbCountryService : ICountryService
    {
        private readonly IDbService dbService;
        private readonly CountryFactory factory;

        public DbCountryService(IDbService dbService, CountryFactory factory)
        {
            this.dbService = dbService;
            this.factory = factory;
        }
        public async Task<CountryView> GetCountry(int id)
        {
            return await factory.GetViewFromDb(await dbService.GetCountry(id));
        }

        public async Task<List<CountryView>> GetCountryList(Func<Country, bool>? where = null, Func<Country, object>? orderBy = null)
        {
            List<Country> dbList = await dbService.GetCountryList(where, orderBy);
            List<CountryView> viewList = new List<CountryView>();

            for (int i = 0; i < dbList.Count; i++)
            {
                viewList.Add(await factory.GetViewFromDb(dbList[i]));
            }

            return viewList;
        }
    }
}
