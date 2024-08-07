using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Services.Models;

public partial class FilmGroupView
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Type { get; set; } = null!;

    public List<FilmView> Films { get; set; } = null!;
}
