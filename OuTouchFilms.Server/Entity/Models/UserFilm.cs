﻿using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Entity.Models;

public partial class UserFilm
{
    public int Id { get; set; }

    public int FilmId { get; set; }

    public int UserId { get; set; }

    public string TypeOfUserFilm { get; set; } = null!;

    public DateTime AddedDate { get; set; }

    public virtual Film Film { get; set; } = null!;
}