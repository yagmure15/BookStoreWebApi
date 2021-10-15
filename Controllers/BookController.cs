using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.BookOperations.DeleteBook;
using BookStoreWebApi.BookOperations.GetBookDetail;
using BookStoreWebApi.BookOperations.GetBooks;
using BookStoreWebApi.BookOperations.UpdateBook;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace BookStoreWebApi.Controllers
{
    [ApiController] //http response döneceğini bildirdik
    [Route("[controller]s")] // endpoint bilgisi
    public class BookController : ControllerBase
    {
        
        
        /*
        private static List<Book> BookList = new List<Book>()
        {
            new Book
            {
                Id =1,
                Title = "Geleceğin Fiziği",
                GenreId = 1, //science fiction
                PageCount = 600,
                PublishDate = new DateTime(2011,06,10)
            },
            new Book
            {
                Id =2,
                Title = "Lean Startup",
                GenreId = 2, //personal growth
                PageCount = 350,
                PublishDate = new DateTime(2001,08,25)
            },
            new Book
            {
                Id =3,
                Title = "Kozmos",
                GenreId = 1, //science fiction
                PageCount = 450,
                PublishDate = new DateTime(2005,07,18)
            },
        };
        */

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        
        [HttpGet]
        public IActionResult GetBooks()
        { 
            // var bookList = BookList.OrderBy(x => x.Id).ToList();
            //  var bookList = _context.Books.OrderBy(x => x.Id).ToList();
            // return bookList;

            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);



        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            BookDetailViewModel result;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            try
            { 
                query.BookId = id;
                validator.ValidateAndThrow(query);
                result = query.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
          
            return Ok(result);
        }
        
        /*
        [HttpGet]  // yukarıda da aynı ifade oldugundan bu şekilde çalışamaz. [HttpGet] modeli benzersiz olmalıdır.
        public Book Get([FromQuery] string id))
        {
            var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }
        */

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
               /*
                ValidationResult result =  validator.Validate(command);
                if (!result.IsValid)
                {
                    foreach (var item in result.Errors)
                    {
                        Console.WriteLine("Özellik: "+ item.PropertyName +" Mesaj: " + item.ErrorMessage);
                    }
                }
                else
                {
                    command.Handle();
                }
                */
               
               validator.ValidateAndThrow(command);
               command.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            try
            {
                command.BookId = id;
                command.Model = updatedBook;
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            try
            {
                command.BookId = id;
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

    }
    
}