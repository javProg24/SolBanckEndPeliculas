using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int GenreId { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public bool IsAvailable { get; set; }

    public double? Rating { get; set; }

    public string? Poster { get; set; }

    public decimal? Budget { get; set; }

    public bool Active { get; set; }

    public virtual Genre Genre { get; set; } = null!;
}
