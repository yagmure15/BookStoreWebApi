using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.BookOperations.DeleteBook;
using BookStoreWebApi.BookOperations.GetBookDetail;
using BookStoreWebApi.BookOperations.GetBooks;
using BookStoreWebApi.BookOperations.UpdateBook;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        
        
        [HttpGet]
        public IActionResult GetBooks()
        { 
            // var bookList = BookList.OrderBy(x => x.Id).ToList();
            //  var bookList = _context.Books.OrderBy(x => x.Id).ToList();
            // return bookList;

            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);



        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context);
            BookDetailViewModel result;
            try
            { 
                query.BookId = id;
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
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
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
            try
            {
                command.BookId = id;
                command.Model = updatedBook;
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

            try
            {
                command.BookId = id;
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