using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Entity.Models;

public partial class FilmGroup
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int? ConditionId { get; set; }

    public string AnimeIds { get; set; } = null!;

    public virtual Condition? Condition { get; set; }
}
