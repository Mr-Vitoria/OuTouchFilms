﻿using OuTouchFilms.Models;
using System.Xml.Linq;

namespace OuTouchFilms.Services
{
    public interface IFilmService
    {
        public Task<object> GetSearchModel(string[] currentGenres, string[] currentCountries, string sortBy = "Name", int currMinYear = -1, int currMaxYear = -1);

        public Task<object> getLastFilmsById(int count);
        public Task<object> getRandomFilms(int count);
        public Task<object> getLastFilmsByDate(int count);
        public Task<object> getFilmInformation(int filmId, int userId = -1);
        public Task<object> getAllFilms(int count, int page, string sortBy, string[] genres, string[] countries, int minYear, int maxYear);
        public Task<List<object>> getFilmsByTitle(string title, int count);
        public Task<List<Film>> getMinimalFilmsByTitle(string title, int count);
        public Task<List<Film>> GetLastUserFilms(int count, int userId = -1);


        public Task<bool> AddFilmsByTitle(string title);
        public Task<bool> AddFullFilmsInformation(int id);
    }
}
