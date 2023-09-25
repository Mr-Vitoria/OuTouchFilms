using OuTouchFilms.Models;

namespace OuTouchFilms.Services
{
    public interface IMailService
    {
        public Task RegistrationLetter(User user);
        public Task NewPostLetter(News news, string userNameReceiver, string emailReceiver);
    }
}
