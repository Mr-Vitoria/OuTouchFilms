namespace OuTouchFilms.Models
{
    public class FilmStaff
    {
        public int Id { get; set; }
        public string Profession { get; set; }
        public string? Description { get; set; }

        public int StaffId { get; set; }
        public int FilmId { get; set; }

        public Staff Staff { get; set;}
        public Film Film { get; set; }
    }
}
