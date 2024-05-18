using BookstoreClassLibrary.Models;
using BookstoreWebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookstoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthorController (ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthorsAsync()
        {
            return await _dbContext.Authors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorByIdAsync(int id)
        {
            var author = await _dbContext.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpGet("byName")]
        public async Task<IActionResult> GetAuthorsByNameAsync(string query)
        {
            var authors = await _dbContext.Authors.Include(a => a.Books).Where(a => a.Name.Contains(query)).ToListAsync();
            if (authors == null)
            {
                return NotFound("Author not found");
            }
            return Ok(authors);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Author>> AddAuthorAsync(Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            foreach (var book in author.Books)
            {
                _dbContext.Entry(book).State = EntityState.Unchanged;
            }
            _dbContext.Authors.Add(author);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthorAsync(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(author).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorAsync(int id)
        {
            var author = await _dbContext.Authors.FindAsync(id);
            if (author == null) 
            {
                return NotFound();
            }
            _dbContext.Authors.Remove(author);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
