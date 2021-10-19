using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using FluentValidation;

namespace BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailQueryValidator()
        {
            RuleFor(command => command.AuthorId).NotNull().GreaterThan(0);

        }
    }
}