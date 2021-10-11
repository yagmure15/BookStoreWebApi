using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public List<Book> GetBooks()
        {
           // var bookList = BookList.OrderBy(x => x.Id).ToList();
            var bookList = _context.Books.OrderBy(x => x.Id).ToList();
            return bookList;
        }
        [HttpGet("{id}")]
        public Book GetById(int id)
        {
           // var book = BookList.Where(book => book.Id == id).SingleOrDefault();
           var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();

            return book;
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
        public IActionResult AddBook([FromBody] Book newBook)
        {
            //var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
            var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);
            if (book is not null)
            {
                return BadRequest();
            }
            //BookList.Add(newBook);
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            //var book = BookList.SingleOrDefault(x => x.Id == id);
            var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if (book is null)
            {
                return BadRequest();
            }

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            // var book = BookList.SingleOrDefault(x => x.Id == id);
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }

            //BookList.Remove(book);
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();

        }

    }
    
}