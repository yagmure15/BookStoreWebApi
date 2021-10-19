using System;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.UpdateAuthorCommand
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        public int AuthorId  { get; set; }
        
        public UpdateAuthorModel Model  { get; set; }

        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.FirstOrDefault(x => x.Id == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Böyle bir Id değerne sahip yazar yok!");
            }

            author.Name = Model.Name == "" ? author.Name : Model.Name;
            author.Birthday = Model.Birthday == default ? author.Birthday : Model.Birthday;
            author.Surname = Model.Surname == "" ? author.Surname : Model.Surname;

            _context.SaveChanges();

        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
    }
}