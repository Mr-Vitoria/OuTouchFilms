using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Entity.Models;

public partial class Translation
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int KodikId { get; set; }
}
