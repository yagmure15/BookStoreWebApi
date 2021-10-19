using AutoMapper;
using BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor;
using BookStoreWebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStoreWebApi.Application.AuthorOperations.Commands.UpdateAuthorCommand;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthors;
using BookStoreWebApi.Application.GenreOperations.Commands.DeleteGenre;
using BookStoreWebApi.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            return Ok(query.Handle());
        }

        [HttpGet("id")]
        public ActionResult GetAuthorById(int id)
        {
            
            GetAuthorDetailQuery query = new GetAuthorDetailQuery( _mapper ,_context);
            query.AuthorId = id;
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_mapper, _context);
            command.Model = newAuthor;

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();

        }
        
        [HttpPut("id")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = id;
            command.Model = updateAuthor;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            
            command.Handle();
            return Ok();

        }
        
        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            
            command.Handle();
            return Ok();
        }
    }
}