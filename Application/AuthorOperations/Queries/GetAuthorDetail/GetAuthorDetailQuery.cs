using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _dbContext;
        public int AuthorId { get; set; }
        public  GetAuthorDetailQueryModel Model { get; set; }

        public GetAuthorDetailQuery(IMapper mapper, BookStoreDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public GetAuthorDetailQueryModel Handle()
        {
            var author = _dbContext.Authors.Include(x=> x.Book)
                .Where(author => author.Id == AuthorId).SingleOrDefault();
            if (author is null)
            {
                throw new InvalidOperationException("Author is not found");
            }
            
            GetAuthorDetailQueryModel vm =  _mapper.Map<GetAuthorDetailQueryModel>(author);
            return vm;

        }
        
    }

    public class GetAuthorDetailQueryModel
    {
        public DateTime Birthday { get; set; }
        
       // public List<Book> Book { get; set; }

        public string FullName { get; set; }
    }
}