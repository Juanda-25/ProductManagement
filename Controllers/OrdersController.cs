using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public OrdersController(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            order.CreatedAt = DateTime.Now;
            order.Status = "Pendiente";
            order.Total = order.Items.Sum(i => i.Price * i.Quantity);

            foreach (var item in order.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null) return BadRequest($"Producto {item.ProductId} no encontrado");
                if (product.Stock < item.Quantity) return BadRequest($"Stock insuficiente para {product.Nombre}");
                product.Stock -= item.Quantity;
                item.ProductName = product.Nombre;
                item.Price = product.Precio;
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Enviar email de confirmación
            await _emailService.SendOrderConfirmationAsync(
                order.CustomerEmail,
                order.CustomerName,
                order.Id,
                order.Total
            );

            return Ok(new { message = "Orden creada exitosamente", orderId = order.Id, total = order.Total });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.Include(o => o.Items).OrderByDescending(o => o.CreatedAt).ToListAsync();
            return Ok(orders);
        }

        [HttpPut("{id}/status")]
        [Authorize]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            order.Status = status;
            await _context.SaveChangesAsync();
            return Ok(order);
        }
    }
}