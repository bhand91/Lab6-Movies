using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HandLab6
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MovieContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

            using (var db = new MovieContext())
            {
                Studio studio = new Studio
                {
                    Name = "20th Century Fox",
                    Movies = new List<Movie>
                    {
                        new Movie
                        {
                            Title = "Avater",
                            Genre = "Action"

                        },
                    new Movie
                        {
                            Title = "Deadpool",
                            Genre = "Action"
                        },
                        new Movie
                        {
                            Title = "Apollo 13",
                            Genre = "Drama"
                        }, 
                        new Movie
                        {
                            Title = "The Martian",
                            Genre = "Sci-Fi"
                        },
                    }
                };

                db.AddRange(studio);
                db.SaveChanges();
            }
        
            using (var db = new MovieContext())
            {
                List<Studio> Studios = new List<Studio>
                {

                    new Studio {Name = "Universal Pictures"}
                };

                db.Add(Studios);
                db.SaveChanges();
            }

            using (var db = new MovieContext())
            {
                Movie movie = new Movie {Title = "Jurassic Park", Genre = "Action"};
                Studio studioToUpdate = db.Studios.Include(s => s.Movies).Where(s => s.Name == "Universal Pictures").First();
                studioToUpdate.Movies.Add(movie);
                db.SaveChanges();
            }

            using (var db = new MovieContext())
            {
                Movie movie = db.Movies.Where(s => s.Title == "Apollo 13").First();
                movie.Studio = db.Studios.Where(m => m.Name == "Universal Pictures").First();
                db.SaveChanges();
            }
        }
    }
}
