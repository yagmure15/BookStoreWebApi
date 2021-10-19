using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetAuthorModel> Handle()
        {
            var authorList = _context.Authors.Include(x => x.Book)
                .OrderBy(x => x.Id).ToList();
            List<GetAuthorModel> vm = _mapper.Map<List<GetAuthorModel>>(authorList);
            return vm;
        }
    }

    public class GetAuthorModel
    {
        public int Id { get; set; }
        
        public DateTime Birthday { get; set; }
        
        public string FullName { get; set; }

    }
}