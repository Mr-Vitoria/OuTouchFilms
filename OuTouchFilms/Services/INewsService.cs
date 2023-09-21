using OuTouchFilms.Models;

namespace OuTouchFilms.Services
{
    public interface INewsService
    {
        public Task<List<News>> getLastNews(int count=-1);
        public Task<List<News>> getInterestingNews();
    }
}
