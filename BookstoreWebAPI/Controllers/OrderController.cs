using BookstoreWebAPI.Data;
using BookstoreWebAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookstoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
            return order;
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
