using OuTouchFilms.Server.Entity;
using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Factories;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Services.DbServices
{
    public class DbStaffService : IStaffService
    {
        private readonly IDbService dbService;
        private readonly StaffFactory factory;

        public DbStaffService(IDbService dbService, StaffFactory factory)
        {
            this.dbService = dbService;
            this.factory = factory;
        }
        public async Task<StaffView> GetStaff(int id)
        {
            return await factory.GetViewFromDb(await dbService.GetStaff(id));
        }

        public async Task<List<StaffView>> GetStaffList(Func<Staff, bool>? where = null, Func<Staff, object>? orderBy = null)
        {
            List<Staff> dbList = await dbService.GetStaffList(where, orderBy);
            List<StaffView> viewList = new List<StaffView>();

            for (int i = 0; i < dbList.Count; i++)
            {
                viewList.Add(await factory.GetViewFromDb(dbList[i]));
            }

            return viewList;
        }
    }
}
