using AutoMapper;
using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.BookOperations.GetBookDetail;
using BookStoreWebApi.BookOperations.GetBooks;
using BookStoreWebApi.Entity;

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
                        opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString() + " Engin"));
            CreateMap<Book, BooksViewModel>().ForMember(des =>
                    des.Genre,
                opt =>
                    opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString() + " Erkan"));

        }
    }
}