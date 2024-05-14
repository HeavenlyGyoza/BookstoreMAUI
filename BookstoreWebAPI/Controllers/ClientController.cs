using BookstoreWebAPI.Data;
using BookstoreWebAPI.Data.Models;
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

        [HttpGet]
        public async Task <ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            var client = await _dbContext.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return client;
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
    }
}
