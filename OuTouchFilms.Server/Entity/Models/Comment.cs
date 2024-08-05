using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Entity.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int FilmId { get; set; }

    public string Text { get; set; } = null!;

    public virtual Film Film { get; set; } = null!;
}
