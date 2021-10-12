using System.Collections.Generic;
using System.Linq;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entity;
using BookStoreWebApi.Utils;

namespace BookStoreWebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _bookStoreDbContext;

        public GetBooksQuery(BookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _bookStoreDbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();

            foreach (var bk in bookList)
            {
                vm.Add(new BooksViewModel()
                    {
                        Title = bk.Title,
                        Genre = ((GenreEnum)bk.GenreId).ToString(),
                        PageCount = bk.PageCount,
                        PublishDate = bk.PublishDate.Date.ToString("dd/MM/yyyy")
                            
                    }
                    
                    );
            }

            return vm;
        }

    }

    public class BooksViewModel
    {
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Title { get; set; }

    }
}