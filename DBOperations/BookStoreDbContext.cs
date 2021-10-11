using BookStoreWebApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebApi.DBOperations
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        { } // bu constructur ne işe yarıyor?
        
        public DbSet<Book> Books { get; set; }
        
    }
}