﻿using System;
using System.Linq;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Book is not  found!");
            }

            //BookList.Remove(book);
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}