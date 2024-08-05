using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OuTouchFilms.Models;
using OuTouchFilms.Services;

namespace OuTouchFilms.Controllers
{
    public class NewsController : Controller
    {
        private readonly OuTouchDbContext context;
        private readonly IMailService mailService;
        private readonly INewsService newsService;

        public NewsController(OuTouchDbContext context, INewsService newsService, IMailService mailService)
        {
            this.context = context;
            this.newsService = newsService;
            this.mailService = mailService;
        }

        public async Task<IActionResult> NewsDetail(int newsId, string lastUrl)
        {
            //Проверка сервисной инфы
            await ServicesInfoService.AddCountFilmsVisit(context, HttpContext);


            await context.Users.LoadAsync();
            return View(new
            {
                news = await context.News.FindAsync(newsId),
                lastUrl = lastUrl
            });
        }

        public async Task<IActionResult> NewsList()
        {
            //Проверка сервисной инфы
            await ServicesInfoService.AddCountFilmsVisit(context, HttpContext);


            var news = await newsService.getLastNews();
            return View(news);
        }

        public async Task<IActionResult> FAQ()
        {
            //Проверка сервисной инфы
            await ServicesInfoService.AddCountFilmsVisit(context, HttpContext);


            return View(await newsService.getInterestingNews());
        }
    }
}
