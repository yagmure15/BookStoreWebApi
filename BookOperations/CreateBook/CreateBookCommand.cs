using System;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStoreWebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookModel Model { get; set; }

        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
            {
                throw new InvalidOperationException("Book is already exist");
            }

            book = _mapper.Map<Book>(Model); // model ile gelen veriyi book nesnesine çevir.
            
            //book = new Book();
            //book.Title = Model.Title;
            //book.GenreId = Model.GenreId;
            //book.PageCount = Model.PageCount;
            // book.PublishDate = Model.PublishDate;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
        
       
    }
    public class CreateBookModel
    {
            
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}