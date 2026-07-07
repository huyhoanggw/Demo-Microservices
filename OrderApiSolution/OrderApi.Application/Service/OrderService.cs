using OrderApi.Application.Conversion;
using OrderApi.Application.DTOS;
using OrderApi.Application.Interface;
using OrderApi.Domain.Entities;
using Polly;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Service
{
    public class OrderService(IOrder Orderinterface,HttpClient client , ResiliencePipelineProvider<string> pipeline) : IOrderService
    {   
        public async Task<ProductDto> GetProduct(int productId) 
        {
            var getProduct = await client.GetAsync($"/api/products/{productId}");
            if (!getProduct.IsSuccessStatusCode) return null!;
            var product = await getProduct.Content.ReadFromJsonAsync<ProductDto>();
            return product!;
        }
        public async Task<UserDto> GetUser(int userId) 
        {
            var getUser = await client.GetAsync($"api/authentication/{userId}");
            if (!getUser.IsSuccessStatusCode) return null!;
            var User = await getUser.Content.ReadFromJsonAsync<UserDto>();
            return User!;
        }
        public async  Task<IEnumerable<Order>> GetOrdersByClientId(int clientId)
        {
            var orders = await Orderinterface.GetOrdersAsync(x => x.ClientId == clientId);
            if (!orders.Any()) 
            {
                return null!;
            }

            return orders; 
        }

        public async Task<OrderDetailDto> GetOrderDetail(int OrderId)
        {
            var order = await Orderinterface.FindByIdAsync(OrderId);
            if (order is null || order.Id <= 0) return null!;
            var retryPipeline = pipeline.GetPipeline("my-retry-pipeline");
            var productDto = await retryPipeline.ExecuteAsync(async token => await GetProduct(order.ProductId));
            var UserDto = await retryPipeline.ExecuteAsync(async token => await GetUser(order.ClientId));
            return new OrderDetailDto
            (
               order.Id,
                 productDto.Id,
                 UserDto.Id,
                UserDto.UserName,
                 UserDto.Email,
               UserDto.Address,
                UserDto.TelePhone,
               productDto.Name,
               order.PurchaseQuanlity,
               productDto.Price,
               productDto.Quanlity * order.PurchaseQuanlity,
               order.OrderedDated
           );
            

        }
    }
}
