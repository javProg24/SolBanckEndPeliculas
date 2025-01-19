using Data.Contexts;
using Data.Models;
using Microsoft.EntityFrameworkCore;

using (var context = new MoviesContext())
{
    //var movie = new Movie();
    //movie.Title = "Moana2";
    //movie.GenreId = 3;
    //movie.ReleaseDate = new DateOnly(2024, 7, 2);
    //movie.IsAvailable = true;
    //movie.Rating = 110000;
    //movie.Active = true;
    //movie.Poster = "sdeefef.jpg";
    //context.Movies.Add(movie);
    //context.SaveChanges();

    var movies = context.Movies.Include(m => m.Genre).ToList();
    foreach (var peli in movies)
    {
        Console.WriteLine(peli.Title + " - genero: " + peli.Genre.Name);
    }
}