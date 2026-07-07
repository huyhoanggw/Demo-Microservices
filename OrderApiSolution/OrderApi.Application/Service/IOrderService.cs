using OrderApi.Application.DTOS;
using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Service
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByClientId(int clientId);
        Task<OrderDetailDto> GetOrderDetail(int OrderId);
    }
}
