using BookstoreClassLibrary.Models;
using BookstoreWebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BookstoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;

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
        public async Task<ActionResult<Book>> AddBook([FromForm] string bookJson, IFormFile coverImage)
        {
            if (string.IsNullOrEmpty(bookJson) || coverImage == null)
            {
                return BadRequest();
            }
            var book = JsonSerializer.Deserialize<Book>(bookJson);
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

            //Save book to get Id
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();

            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Upload", "BookCovers", book.Id.ToString());
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string imagePath = Path.Combine(filePath, "cover.jpeg");
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await coverImage.CopyToAsync(stream);
            }
            book.CoverImagePath = imagePath;

            _dbContext.Entry(book).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromForm] string bookJson, IFormFile coverImage)
        {
            if (id <= 0 || string.IsNullOrEmpty(bookJson) || coverImage == null)
            {
                return BadRequest();
            }
            var book = JsonSerializer.Deserialize<Book>(bookJson);
            if (id != book.Id)
            {
                return BadRequest();
            }

            if (coverImage != null)
            {
                if (!string.IsNullOrEmpty(book.CoverImagePath) && System.IO.File.Exists(book.CoverImagePath))
                {
                    System.IO.File.Delete(book.CoverImagePath);
                }

                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Upload", "BookCovers", book.Id.ToString());
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string imagePath = Path.Combine(filePath, "cover.jpeg");
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(stream);
                }
                book.CoverImagePath = imagePath;
            }

            _dbContext.Entry(book).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(book.CoverImagePath) && System.IO.File.Exists(book.CoverImagePath)) 
            {
                System.IO.File.Delete(book.CoverImagePath);
            }

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
