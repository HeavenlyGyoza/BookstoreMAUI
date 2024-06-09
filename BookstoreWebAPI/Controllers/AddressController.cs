using BookstoreClassLibrary.Models;
using BookstoreWebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookstoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public AddressController (ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAllAddresses()
        {
            return await _dbContext.Addresses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddressById(int id)
        {
            var address = await _dbContext.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }

        [HttpGet("byClientId/{clientId}")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddressesByClientId(int clientId)
        {
            if (clientId > 0) 
            {
                var addresses = await _dbContext.Addresses.Where(a => a.Clients.Any(c => c.Id == clientId)).ToListAsync();
                return Ok(addresses);
            }
            else
            {
                return BadRequest("clientId is < 0");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<Address>> AddAddress(Address address)
        {
            if (address == null)
            {
                return BadRequest("Invalid address data.");
            }

            int clientId = address.Clients.FirstOrDefault()?.Id ?? 0;
            if (clientId <= 0)
            {
                return BadRequest("No client associated with the address");
            }
            var existingClient = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);
            if (existingClient == null)
            {
                return NotFound("Client not found");
            }

            var newAddress = new Address
            {
                Street = address.Street,
                AddInfo = address.AddInfo,
                City = address.City,
                PostalCode = address.PostalCode,
                Province = address.Province,
                State = address.State,
                Country = address.Country,
                IsPrimary = address.IsPrimary
            };

            existingClient.Addresses ??= new List<Address>();
            existingClient.Addresses.Add(newAddress);

            await _dbContext.SaveChangesAsync();

            return Ok(newAddress);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(address).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _dbContext.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            _dbContext.Addresses.Remove(address);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
