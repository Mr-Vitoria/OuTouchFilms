﻿namespace OuTouchFilms.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string BackImgUrl { get; set; }
        public bool IsInteresting { get; set; }
        public string Type { get; set; } = "Film";

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
