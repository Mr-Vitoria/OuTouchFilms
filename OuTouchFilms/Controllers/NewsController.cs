using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OuTouchFilms.Models;
using OuTouchFilms.Services;

namespace OuTouchFilms.Controllers
{
    public class NewsController : Controller
    {
        private readonly OuTouchDbContext context;
        //private readonly IMailService mailService;
        private readonly INewsService newsService;

        public NewsController(OuTouchDbContext context, INewsService newsService)
        {
            this.context = context;
            this.newsService = newsService;
        }

        public async Task<IActionResult> NewsDetail(int newsId, string lastUrl)
        {
            await context.Users.LoadAsync();
            return View(new
            {
                news = await context.News.FindAsync(newsId),
                lastUrl = lastUrl
            });
        }

        public async Task<IActionResult> AddNews(string title, string text, string backImg="", string lastUrl = "/")
        {
            if (HttpContext.Request.Cookies.ContainsKey("id"))
            {
                int userId = int.Parse(HttpContext.Request.Cookies["id"]);

                News news = new News()
                {
                    Title = title,
                    Text = text,
                    UserId = userId,
                    BackImgUrl = backImg,
                    Date = DateTime.Now.ToString("dd.MM.yyyy")
                };

                await context.News.AddAsync(news);

                await context.SaveChangesAsync();
            }

            

            return Redirect(lastUrl);

        }

        public async Task<IActionResult> NewsList()
        {
            var news = await newsService.getLastNews();
            return View(news);
        }

        public async Task<IActionResult> FAQ()
        {
            return View(await newsService.getInterestingNews());
        }
    }
}
