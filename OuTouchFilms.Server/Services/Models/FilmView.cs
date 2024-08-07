using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Services.Models;

public partial class FilmView
{
    public int Id { get; set; }

    public int KinopoiskId { get; set; }

    public string? ImdbId { get; set; }

    public string? Title { get; set; }

    public string? OriginalTitle { get; set; }

    public string? Poster { get; set; }

    public string? CoverPoster { get; set; }

    public int Year { get; set; }

    public string? Duration { get; set; }

    public string? Description { get; set; }

    public string? Annotation { get; set; }

    public string? Status { get; set; }

    public string Type { get; set; } = null!;

    public List<CountryView> Countries { get; set; } = null!;

    public List<GenreView> Genres { get; set; } = null!;

    public string? Slogan { get; set; }

    public float? ImdbRating { get; set; }

    public float? KinopoiskRating { get; set; }

    public List<StaffView>? Actors { get; set; }

    public List<StaffView>? Composers { get; set; }

    public List<StaffView>? Designs { get; set; }

    public List<StaffView>? Editors { get; set; }

    public List<StaffView>? Operators { get; set; }

    public List<StaffView>? Producers { get; set; }

    public List<StaffView>? Writers { get; set; }

    public List<StaffView>? Directors { get; set; }

    public List<string>? Screenshots { get; set; }

    public DateTime? LastUpdate { get; set; }

    public virtual ICollection<CommentView> Comments { get; set; } = new List<CommentView>();

    public bool isUserPlan { get; set; } = false;
}
