using BookstoreClassLibrary.Models;
using BookstoreWebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookstoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public ClientController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("all")]
        public async Task <ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Client>> AddClient(Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }
            var existingclient = await _dbContext.Clients.FirstOrDefaultAsync(u => u.Email == client.Email);
            if (existingclient != null)
            {
                return Conflict("User with the same email already exists.");
            }
            client.Adresses = client.Adresses ?? new List<Address>();
            client.Orders = client.Orders ?? new List<Order>();
            _dbContext.Clients.Add(client);
            _dbContext.SaveChangesAsync();
            return Ok(client);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClient(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(client).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var client = await _dbContext.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] Client client)
        {
            if (client == null || string.IsNullOrEmpty(client.Email) || string.IsNullOrEmpty(client.Password))
            {
                return BadRequest("Invalid client request");
            }

            var existingClient = await _dbContext.Clients
                .SingleOrDefaultAsync(u => u.Email == client.Email);

            if (existingClient == null || existingClient.Password != client.Password)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(existingClient);
        }
    }
}
