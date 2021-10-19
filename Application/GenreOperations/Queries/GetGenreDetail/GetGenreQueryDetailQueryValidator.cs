using FluentValidation;

namespace BookStoreWebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreQueryDetailQueryValidator : AbstractValidator<GetGenreQueryDetailQuery>
    {
        public GetGenreQueryDetailQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThanOrEqualTo(0);
        }
    }
}