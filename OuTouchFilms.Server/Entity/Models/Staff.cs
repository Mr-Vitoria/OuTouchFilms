using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Entity.Models;

public partial class Staff
{
    public int Id { get; set; }

    public int SwaggerId { get; set; }

    public string Name { get; set; } = null!;

    public string Poster { get; set; } = null!;

    public virtual ICollection<FilmStaff> FilmStaffs { get; set; } = new List<FilmStaff>();
}
