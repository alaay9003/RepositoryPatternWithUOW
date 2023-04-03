using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;

namespace RepositoryPatternWithUOW.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBaseRepository<Book> _bookRepository;

        public BooksController(IBaseRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_bookRepository.GetById(1));
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_bookRepository.GetAll());
        }
        [HttpGet("GetByName")]
        public IActionResult GetByName()
        {
            return Ok(_bookRepository.Find(b=>b.Title=="drama",new[] {"Author"}));
        }
        [HttpGet("GetAllWithAuthor")]
        public IActionResult GetAllWithAuthor()
        {
            return Ok(_bookRepository.Find(b => b.Title == "drama", new[] { "Author" }));
        }
        [HttpGet("GetOrderd")]
        public IActionResult GetOrderd()
        {
            return Ok(_bookRepository.FindAll(b => b.Title.Contains("comedy"),null,null,b=>b.Id));
        }
        [HttpPost("AddBook")]
        public IActionResult AddBook( )
        {
            return Ok(_bookRepository.Add(new Book { Title="test",AuthorId=1}));
        }
    }
}
