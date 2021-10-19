using AutoMapper;
using BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor;
using BookStoreWebApi.Application.AuthorOperations.Commands.UpdateAuthorCommand;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthors;
using BookStoreWebApi.Application.BookOperations.Commands.CreateBook;
using BookStoreWebApi.Application.BookOperations.Queries.GetBookDetail;
using BookStoreWebApi.Application.BookOperations.Queries.GetBooks;
using BookStoreWebApi.Application.GenreOperations.Queries.GetGenreDetail;
using BookStoreWebApi.Application.GenreOperations.Queries.GetGenres;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>(); // 2. paramatre hedeftir. CreateBookModel objesi Book objesine maplanabilir olsun demek.
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(des =>
                        des.Genre,
                    opt =>
                        opt.MapFrom(src => src.Genre.Name ));
            CreateMap<Book, BooksViewModel>().ForMember(des =>
                    des.Genre,
                opt =>
                    opt.MapFrom(src => src.Genre.Name));

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateAuthorModel,Author>();
            
            CreateMap<Author, GetAuthorModel>();
            CreateMap<UpdateAuthorModel, Author>();
            CreateMap<Author, GetAuthorDetailQueryModel>();

        }
    }
}