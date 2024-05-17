﻿using BookstoreClassLibrary.Models;
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

        [HttpPost("add")]
        public async Task<ActionResult<Address>> AddAddress(Address address)
        {
            if (address == null)
            {
                return BadRequest();
            }
            _dbContext.Addresses.Add(address);
            _dbContext.SaveChangesAsync();
            return Ok();
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
