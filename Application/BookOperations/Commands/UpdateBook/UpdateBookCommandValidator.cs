using System;
using FluentValidation;

namespace BookStoreWebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.Model.Title).MinimumLength(2);
            RuleFor(command => command.Model.GenreId).GreaterThanOrEqualTo(0);
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);

        }
    }
}