using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreQueryDetailQuery
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreQueryDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive == true && x.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Book type is not found!");
            }
            GenreDetailViewModel returnObj = _mapper.Map<GenreDetailViewModel>(genre);
            return returnObj;
        }
    }
    
    
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}