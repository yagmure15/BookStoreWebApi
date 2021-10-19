using System;
using System.Linq;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Updated genre is not found");
            }

            if (_context.Genres.Any(x=> x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            {
                throw new InvalidOperationException("The book of the same name already exists");
            }

            genre.Name = Model.Name.Trim() == "" ?  genre.Name : Model.Name ;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
        
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}