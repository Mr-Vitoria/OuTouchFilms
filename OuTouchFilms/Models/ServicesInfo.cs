namespace OuTouchFilms.Models
{
    public class ServicesInfo
    {
        //Main property
        public int Id { get; set; }
        public DateOnly Date { get; set; }

        //Films property
        public int countVisitsFilm { get; set; } = 0;
    }
}
