using BookstoreClassLibrary.Models;
using BookstoreWebAPI.Data;
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
            return await _dbContext.Books.Include(b => b.Authors).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _dbContext.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            var authors = new List<Author>();
            //Checks if author already exists to avoid duplicates
            foreach (var author in book.Authors)
            {
                var existingAuthor = await _dbContext.Authors.FirstOrDefaultAsync(a => a.Name == author.Name);
                if (existingAuthor != null) 
                {
                    authors.Add(existingAuthor);
                }
                else
                {
                    authors.Add(author);
                }
            }
            book.Authors = authors;
            _dbContext.Books.Add(book);
            _dbContext.SaveChangesAsync();
            return Ok();
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
            var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
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
