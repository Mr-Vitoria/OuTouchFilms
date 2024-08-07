using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Services.Models;

public partial class StaffView
{
    public int Id { get; set; }

    public int SwaggerId { get; set; }

    public string Name { get; set; } = null!;

    public string Poster { get; set; } = null!;
}
