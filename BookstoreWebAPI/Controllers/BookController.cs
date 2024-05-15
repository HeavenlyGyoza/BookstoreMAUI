using BookstoreWebAPI.Data;
using BookstoreWebAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookstoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public BookController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            return await _dbContext.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(book).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
