using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Entity.Models;

public partial class FilmStaff
{
    public int Id { get; set; }

    public string Profession { get; set; } = null!;

    public string? Description { get; set; }

    public int StaffId { get; set; }

    public int FilmId { get; set; }

    public virtual Film Film { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
