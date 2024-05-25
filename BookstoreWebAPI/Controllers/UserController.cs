using BookstoreClassLibrary.Models;
using BookstoreWebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookstoreWebAPI.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public UserController(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<User>>> GettAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("add")]
        public async Task<ActionResult<User>> AddUserAsync(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAsync(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Invalid client request");
            }
            
            var existingUser = _dbContext.Users.SingleOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
            if (existingUser != null)
            {
                return Ok(existingUser);
            }

            return Unauthorized("Invalid credentials");
        }
    }
}
