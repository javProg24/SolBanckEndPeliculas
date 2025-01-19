using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Data.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }
    [JsonIgnore]//evita el ciclo al serializar los objetos en formato json
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
