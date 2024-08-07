using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Services.Models;

public partial class CommentView
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int FilmId { get; set; }

    public string Text { get; set; } = null!;

    public virtual FilmView Film { get; set; } = null!;
}
