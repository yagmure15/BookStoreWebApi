using System;
using System.Linq;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }

        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.FirstOrDefault(x => x.Id == BookId);

            if (book is null)
            {
                throw new InvalidOperationException("Book is not found!");
            }
            
            
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
            
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public DateTime PublishDate { get; set; }
    }
}