using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebApi.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDbContext bookStoreDbContext, IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _bookStoreDbContext.Books.Include(x=> x.Genre)
                .OrderBy(x => x.Id).ToList<Book>();
            
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);
            return vm;
        }

    }

    public class BooksViewModel
    {
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }

    }
}