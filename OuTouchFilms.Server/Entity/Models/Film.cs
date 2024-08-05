using System;
using System.Collections.Generic;

namespace OuTouchFilms.Server.Entity.Models;

public partial class Film
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

    public string Countries { get; set; } = null!;

    public string Genres { get; set; } = null!;

    public string? Slogan { get; set; }

    public float? ImdbRating { get; set; }

    public float? KinopoiskRating { get; set; }

    public string? ActorIds { get; set; }

    public string? ComposerIds { get; set; }

    public string? DesignIds { get; set; }

    public string? EditorIds { get; set; }

    public string? OperatorIds { get; set; }

    public string? ProducerIds { get; set; }

    public string? WriterIds { get; set; }

    public string? DirectorIds { get; set; }

    public string? Screenshots { get; set; }

    public DateTime? LastUpdate { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<FilmStaff> FilmStaffs { get; set; } = new List<FilmStaff>();

    public virtual ICollection<UserFilm> UserFilms { get; set; } = new List<UserFilm>();
}
