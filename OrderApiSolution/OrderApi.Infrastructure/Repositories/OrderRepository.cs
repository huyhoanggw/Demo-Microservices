using eCommerce.SharedLibary.Interface;
using eCommerce.SharedLibary.Logs;
using eCommerce.SharedLibary.Reponse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OrderApi.Application.Interface;
using OrderApi.Domain.Entities;
using OrderApi.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure.Repositories
{
    public class OrderRepository(OrderDbcontext dbcontext) : IOrder
    {
        public async  Task<Reponse> CreateAsync(Order entity)
        {
            var order =  await dbcontext.Order.FindAsync(entity.Id);
            if (order is not null) return new Reponse(false, "Order duplicate Id");
            try
            {
                var orderCurrent = new Order()
                {
                    ClientId = entity.ClientId,
                    ProductId = entity.ProductId,
                    PurchaseQuanlity = entity.PurchaseQuanlity,
                    OrderedDated = entity.OrderedDated,

                };
                await dbcontext.AddAsync(orderCurrent);
                var result = await dbcontext.SaveChangesAsync();
                return result > 0 ? new Reponse(true, "Create order successfully") : new Reponse(false, "Error Occured while placing order");
            }

            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Reponse(false, "Error Occured while placing order");
            }
        }

        public async Task<Reponse> DeleteAsync(int  id)
        {
            try
            {
                var order = await dbcontext.Order.FindAsync(id);
                if (order is null) return new Reponse(false, "Order Id is not found ");
                dbcontext.Order.Remove(order);
                var result = dbcontext.SaveChanges();
                return result > 0 ? new Reponse(true, "Delete Order Successfully") : new Reponse(false, "Error Occured while Delete Order");


            }
            catch (Exception ex) 
            {
                LogException.LogExceptions(ex);
                return new Reponse(false, ex.Message);
            }
        }

        public async Task<Order> FindByIdAsync(int id)
        {
            var order = await dbcontext.Order.FindAsync(id);
            if (order is null) return null!;
            return order; 
                
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var orders = await dbcontext.Order.ToListAsync();
            return orders;
        }

        public async Task<Order> GetByAsync(Expression<Func<Order, bool>> predicate)
        {
                var result = await dbcontext.Order.Where(predicate).FirstOrDefaultAsync();
            return result!; 
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate)
        {
            var result = await dbcontext.Order.Where(predicate).ToListAsync();
            return result;
        }

        public async Task<Reponse> UpdateAsync(Order entity)
        {
            try
            {
                var order = await FindByIdAsync(entity.Id);
                if (order is null) return new Reponse(false, "Order is not found");
                order.OrderedDated = entity.OrderedDated;
                order.ProductId = entity.ProductId;
                order.PurchaseQuanlity = entity.PurchaseQuanlity;
                order.ClientId = entity.ClientId;
                var result = await dbcontext.SaveChangesAsync();
                return result > 0 ? new Reponse(true, "Update Order Successfully") : new Reponse(false, "Error Occured while update");

            }   
            catch(Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Reponse(false, ex.Message);
            }
        }
    }
}
