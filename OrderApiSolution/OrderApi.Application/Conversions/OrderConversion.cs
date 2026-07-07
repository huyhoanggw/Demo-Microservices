using OrderApi.Application.DTOS;
using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Conversion
{
    public static class OrderConversion
    {
        public static Order ToEntity(OrderDto dto)
            => new Order { Id = dto.Id, ClientId = dto.ClientId, OrderedDated = dto.OrderedDate, ProductId = dto.ProductId, PurchaseQuanlity = dto.PurchaseQuanlity };
        public static (OrderDto?, IEnumerable<OrderDto>?) FromOrder(Order order, IEnumerable<Order> orders)
        {
            if (order is not null || orders is null)  
                return (new OrderDto (
                    order.Id,
                    order.ProductId,
                    order.ClientId,
                    order.PurchaseQuanlity,
                    order.OrderedDated
                    ), null);
            if (order is null || orders is not null)
                return (null, orders.Select(p => new OrderDto
               (p.Id,
               p.ProductId,
               p.ClientId,
               p.PurchaseQuanlity,
               p.OrderedDated)).ToList());
            return (null, null);
        }
    }
}
