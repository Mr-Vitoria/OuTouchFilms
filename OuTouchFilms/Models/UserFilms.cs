using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace OuTouchFilms.Models
{
    public enum TypeOfUserFilm
    {
        [Description("Смотрю")]
        [Display(Name = "Смотрю")]
        Watching,
        [Description("Отложено")]
        [Display(Name = "Отложено")]
        OnHold,
        [Description("Запланировано")]
        [Display(Name = "Запланировано")]
        PlanToWatch,
        [Description("Брошено")]
        [Display(Name = "Брошено")]
        Dropped,
        [Description("Просмотрено")]
        [Display(Name = "Просмотрено")]
        Completed,
        [Description("Не определено")]
        [Display(Name = "Не определено")]
        None

    }

    public class UserFilms
    {

        public int Id { get; set; }
        public int FilmId { get; set; }
        public int UserId { get; set; }
        public TypeOfUserFilm TypeOfUserFilm { get; set; }

        public Film Film { get; set; }
        public User User { get; set; }
    }
}
