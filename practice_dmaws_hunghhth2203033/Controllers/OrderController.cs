using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practice_dmaws_hunghhth2203033.Dtos;
using practice_dmaws_hunghhth2203033.Entities;

namespace practice_dmaws_hunghhth2203033.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderSystemContext _context;

        public OrderController(OrderSystemContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrder>> PlaceOrder(CreateOrder createOrder)
        {
            var order = new OrderTbl();
            
                order.ItemName = createOrder.ItemName;
                order.ItemQty = createOrder.ItemQty;
                order.OrderAddress = createOrder.OrderAddress;
                order.OrderDelivery = createOrder.OrderDelivery;
                order.PhoneNumber = createOrder.PhoneNumber;

            _context.OrderTbls.Add(order);
            await _context.SaveChangesAsync();

            return Created("Success", order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrder updatedOrder)
        {
            if (!OrderExists(id))
            {
                return BadRequest();
            }

            var existingOrder = await _context.OrderTbls.FindAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            existingOrder.OrderAddress = updatedOrder.OrderAddress;
            existingOrder.PhoneNumber = updatedOrder.PhoneNumber;

            _context.Entry(existingOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.OrderTbls.Any(e => e.ItemCode == id);
        }
    }
}
