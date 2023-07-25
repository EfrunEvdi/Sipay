using AutoMapper;
using BookStore.Application.AuthorOperation.Commands.CreateAuthor;
using BookStore.Application.AuthorOperation.Commands.DeleteAuthor;
using BookStore.Application.AuthorOperation.Commands.UpdateAuthor;
using BookStore.Application.AuthorOperation.Queries.GetAuthorDetail;
using BookStore.Application.AuthorOperation.Queries.GetAuthors;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        public readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAuthors() // Listeleme
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")] // ID'ye göre getirme
        public ActionResult GetAuthorDetail(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorID = id;
            GetAuthorDetailQueryValidator validations = new GetAuthorDetailQueryValidator();
            validations.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost] // Ekleme
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newGenre)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_mapper, _context);
            command.Model = newGenre;

            CreateAuthorCommandValidator validations = new CreateAuthorCommandValidator();
            validations.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")] // Silme
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorID = id;
            DeleteAuthorCommandValidator cv = new DeleteAuthorCommandValidator(); //validator sınıfını calıştırma
            cv.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")] // Güncelleme
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorID = id;
            command.Model = updatedAuthor;

            UpdateAuthorCommandValidator cv = new UpdateAuthorCommandValidator();
            cv.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}
