using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Services.Models;

public partial class FilmStaffView
{
    public int Id { get; set; }

    public string Profession { get; set; } = null!;

    public string? Description { get; set; }

    public int StaffId { get; set; }

    public int FilmId { get; set; }

    public virtual FilmView Film { get; set; } = null!;

    public virtual StaffView Staff { get; set; } = null!;
}
