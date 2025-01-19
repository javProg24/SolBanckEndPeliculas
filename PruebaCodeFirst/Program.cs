using DataCodeFirst.Contexts;
using DataCodeFirst.Models;
using Microsoft.EntityFrameworkCore;

using (var context = new MovieContexts())
{
    var genre = new Genre();
    genre.Name = "Action";
    genre.Description = "peliculas de accion";
    genre.Active = true;
    context.Genres.Add(genre);
    context.SaveChanges();

    var movie = new Movie();
    movie.Title = "Moana2";
    movie.GenreId = 1;
    movie.ReleaseDate = new DateTime(2024, 7, 2);
    movie.IsAvailable = true;
    movie.Rating = 110000;
    movie.Active = true;
    movie.Poster = "sdeefef.jpg";
    context.Movies.Add(movie);
    context.SaveChanges();

    var movies = context.Movies.Include(m => m.Genre).ToList();
    foreach (var peli in movies)
    {
        Console.WriteLine(peli.Title + " - genero: " + peli.Genre.Name);
    }
}