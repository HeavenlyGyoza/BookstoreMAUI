using BookstoreWebAPI.Data;
using BookstoreWebAPI.Data.Models;
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
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            return await _dbContext.Authors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            var author = await _dbContext.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return author;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
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
        public async Task<IActionResult> DeleteAuthor(int id)
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
