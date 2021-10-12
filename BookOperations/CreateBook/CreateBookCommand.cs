﻿using System;
using System.Linq;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStoreWebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public CreateBookModel Model { get; set; }

        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
            {
                throw new InvalidOperationException("Book is already exist");
            }

            book = new Book();
            book.Title = Model.Title;
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount;
            book.PublishDate = Model.PublishDate;

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