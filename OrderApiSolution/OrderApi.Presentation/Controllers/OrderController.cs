using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Application.Conversion;
using OrderApi.Application.DTOS;
using OrderApi.Application.Interface;
using OrderApi.Application.Service;
using OrderApi.Domain.Entities;

namespace OrderApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController(IOrder repository , IOrderService services): Controller
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await repository.GetAllAsync();
            if (!orders.Any())
            {
                return NotFound();

            }
            var (_, list) = OrderConversion.FromOrder(null, orders);
            return list.Any() ? Ok(list) : NotFound(list); 
                }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto request)
        {
            if (!ModelState.IsValid) return BadRequest("Incomplete data submit");
            var order = OrderConversion.ToEntity(request);

            var result = repository.CreateAsync(order);
            return result.Result.Flag == true ? Ok(order) : BadRequest(order);
        }
        [HttpPut]
        public async Task<ActionResult<Order>> UpdateOrder(OrderDto request)
        {
            if (!ModelState.IsValid) return BadRequest("Incomplete data submit");
            var order = OrderConversion.ToEntity(request);

            var result = repository.UpdateAsync(order);
            return result.Result.Flag == true ? Ok(order) : BadRequest(order);
        }
        [HttpDelete]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var result = repository.DeleteAsync(id);
            return result.Result.Flag == true ? Ok(result) : BadRequest(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var result = await repository.FindByIdAsync(id);
            var (orderDto , _) = OrderConversion.FromOrder(result, null);
            return result is null ? BadRequest(orderDto) : Ok(orderDto);
        }
        [HttpGet("client/{clientid:int}")]
        public async Task<ActionResult<OrderDto>> GetOrdersByClientId(int clientid)
        {
            if (clientid <= 0) return BadRequest();
            var result = await services.GetOrdersByClientId(clientid);
                var (_ , orders) = OrderConversion.FromOrder(null, result);
            return result is null ? BadRequest(orders) : Ok(orders);
        }
        [HttpGet("order/{orderId:int}")]
        public async Task<ActionResult<OrderDto>> GetOrderDetail(int orderId)
        {
            if (orderId <= 0) return BadRequest();
            var result = await services.GetOrderDetail(orderId); 
           
            return result is not null ? Ok(result) : BadRequest(result);
        }
    }
}
