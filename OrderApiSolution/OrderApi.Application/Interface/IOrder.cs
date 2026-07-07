using eCommerce.SharedLibary.Interface;
using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Interface
{
    public interface IOrder : IGenericInterface<Order>
    {
        public Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate);
    }
}
