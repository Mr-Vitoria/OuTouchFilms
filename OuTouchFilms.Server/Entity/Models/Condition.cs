using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Entity.Models;

public partial class Condition
{
    public int Id { get; set; }

    public string PropertyNames { get; set; } = null!;

    public string PropertyValues { get; set; } = null!;

    public string? OrderBy { get; set; }

    public bool? IsReverse { get; set; }

    public virtual ICollection<FilmGroup> FilmGroups { get; set; } = new List<FilmGroup>();
}
