﻿using Microsoft.EntityFrameworkCore;
using OuTouchFilms.Models;

namespace OuTouchFilms.Services
{
    public class NewsService : INewsService
    {
        private readonly OuTouchDbContext context;

        public NewsService(OuTouchDbContext context)
        {
            this.context = context;
        }
        public async Task<List<News>> getLastNews(int count = -1)
        {
            await context.Users.LoadAsync();
            if(count == -1)
            {
                return await context.News.Where(n => n.Type == "Common" || n.Type == "Film").OrderByDescending(news => news.Date).ToListAsync();
            }

            return await context.News.Where(n => n.Type == "Common" || n.Type == "Film").OrderByDescending(news => news.Date).Take(count).ToListAsync();
        }
        public async Task<List<News>> getInterestingNews()
        {

            await context.Users.LoadAsync();

            return await context.News.Where(n => n.Type == "Common" || n.Type == "Film").Where(news => news.IsInteresting == true).ToListAsync();
        }
    }
}
