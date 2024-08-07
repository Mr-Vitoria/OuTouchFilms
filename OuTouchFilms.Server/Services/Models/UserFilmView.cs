using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Services.Models;

public partial class UserFilmView
{
    public int Id { get; set; }

    public int FilmId { get; set; }

    public int UserId { get; set; }

    public string TypeOfUserFilm { get; set; } = null!;

    public DateTime AddedDate { get; set; } = DateTime.Now;

    public virtual FilmView Film { get; set; } = null!;
}
