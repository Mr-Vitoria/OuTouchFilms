using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Services.Models;

public partial class ConditionView
{
    public int Id { get; set; }

    public string PropertyNames { get; set; } = null!;

    public string PropertyValues { get; set; } = null!;

    public string? OrderBy { get; set; }

    public bool? IsReverse { get; set; }

    public virtual ICollection<FilmGroupView> FilmGroups { get; set; } = new List<FilmGroupView>();
}
