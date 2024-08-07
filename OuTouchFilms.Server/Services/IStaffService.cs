using OuTouchFilms.Server.Entity.Models;
using OuTouchFilms.Server.Services.Models;

namespace OuTouchFilms.Server.Services
{
    public interface IStaffService
    {
        public Task<List<StaffView>> GetStaffList(Func<Staff, bool>? where = null, Func<Staff, object>? orderBy = null);
        public Task<StaffView> GetStaff(int id);
    }
}
