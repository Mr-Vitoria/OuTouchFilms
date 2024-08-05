using OuTouchFilms.Models;

namespace OuTouchFilms.Services
{
    public interface IUserService
    {
        public Task<bool> AddUser(User user, HttpContext httpContext);
        public Task<bool> CheckUser(User user, HttpContext httpContext);
        public bool SignOut(HttpContext httpContext);
        public Task<bool> ChangeCurrentUserProperties(HttpContext httpContext, string userImg, string emailSend);
        public Task<object?> GetCurrentUserInfo(HttpContext httpContext, TypeOfUserFilm? TypeOfUserFilm);
        public Task<bool> AddComment(int userId,int filmId,string text);
        public void ChangeTheme(HttpContext httpContext);

        //Validations methods

        public Task<bool> CheckLogin(string login);
        public Task<bool> CheckEmail(string email);
        public bool CheckPassword(string password);
    }
}
