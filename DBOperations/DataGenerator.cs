using System;
using System.Linq;
using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreWebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context =
                new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any()) //burada database'de herhangi bir verinin olup olmadığına bakıyoruz
                {
                    return;
                }
                
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                    );
                context.Authors.AddRange(
                    new Author
                    {
                        Birthday = new DateTime(2011,06,10),
                        Name = "Engin",
                        Surname = "Yağmur",
                    },
                    new Author
                    {
                        Birthday = new DateTime(2011,06,10),
                        Name = "Erkan",
                        Surname = "Yağmur",
                    },
                    new Author
                    {
                        Birthday = new DateTime(2011,06,10),
                        Name = "Gökhan",
                        Surname = "Kocamaz",
                    },
                    new Author
                    {
                        Birthday = new DateTime(2011,06,10),
                        Name = "Melike",
                        Surname = "Yaşar",
                    }
                    
                  
                );
                
                
                context.Books.AddRange(
                    new Book
                    {
                       // Id =1,
                       AuthorId = 1,
                        Title = "Geleceğin Fiziği",
                        GenreId = 1, //science fiction
                        PageCount = 600,
                        PublishDate = new DateTime(2011,06,10)
                    },
                    new Book
                    {
                       // Id =2,
                       AuthorId = 1,
                        Title = "Lean Startup",
                        GenreId = 2, //personal growth
                        PageCount = 350,
                        PublishDate = new DateTime(2001,08,25)
                    },
                    new Book
                    {
                        // Id =3,
                        AuthorId = 2,
                        Title = "Kozmos",
                        GenreId = 1, //science fiction
                        PageCount = 450,
                        PublishDate = new DateTime(2005,07,18)
                    }
                );
                context.SaveChanges();
            }

        }
    }
}