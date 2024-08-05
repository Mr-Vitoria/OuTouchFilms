using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Entity.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;
}
