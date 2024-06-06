using BookstoreClassLibrary.Models;
using BookstoreWebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BookstoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<OrderController> _logger;
        public OrderController(ApplicationDbContext dbContext, ILogger<OrderController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Order>> AddOrder(Order order)
        {
            if (order == null)
            {
                _logger.LogError("Order is null.");
                return BadRequest("Order is null.");
            }

            try
            {
                if (order.ClientId > 0)
                {
                    var client = await _dbContext.Clients.FindAsync(order.ClientId);
                    if (client == null)
                    {
                        return NotFound("Client not found");
                    }
                    order.Client = client;
                }

                if (order.BookId > 0)
                {
                    var book = await _dbContext.Books.FindAsync(order.BookId);
                    if (book == null)
                    {
                        return NotFound("Book not found");
                    }
                    order.Book = book;
                    book.Stock -= order.Quantity;
                    _dbContext.Entry(book).State = EntityState.Modified;
                }

                var existingAddress = await _dbContext.Addresses.FirstOrDefaultAsync(a =>
                                                                a.Street == order.Address.Street &&
                                                                a.City == order.Address.City &&
                                                                a.State == order.Address.State &&
                                                                a.PostalCode == order.Address.PostalCode);

                if (existingAddress != null)
                {
                    order.AddressId = existingAddress.Id;
                    order.Address = existingAddress;
                }
                else
                {
                    _dbContext.Addresses.Add(order.Address);
                }


                _logger.LogInformation($"Order details: {JsonSerializer.Serialize(order)}");

                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync();

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            } 
            _dbContext.Entry(order).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
