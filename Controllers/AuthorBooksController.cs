using System;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor;
using BookStoreWebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStoreWebApi.Application.AuthorOperations.Commands.UpdateAuthorCommand;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthors;
using BookStoreWebApi.Application.BookOperations.Commands.CreateBook;
using BookStoreWebApi.Application.BookOperations.Queries.GetBooks;
using BookStoreWebApi.Application.GenreOperations.Commands.DeleteGenre;
using BookStoreWebApi.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApi.Controllers
{
    [ApiController]
    [Route("author/{authorId}/books")]
    public class AuthorBooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorBooksController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAuthorBooks(int authorId)
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var author = _context.Authors.SingleOrDefault(x => x.Id == authorId);
            if (author is null)
            {
                throw new InvalidOperationException("Yazar yok");
            }
            var books = query.Handle().Where(x => x.AuthorId == authorId);
            return Ok(books);

        }
        
    }
}