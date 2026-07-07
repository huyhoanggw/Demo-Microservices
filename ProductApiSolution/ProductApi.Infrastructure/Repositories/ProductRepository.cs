using eCommerce.SharedLibary.Logs;
using eCommerce.SharedLibary.Reponse;
using Microsoft.EntityFrameworkCore;
using ProductApi.Application.DTOs;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.Repositories
{
    public class ProductRepository(ProductDbcontext context) : IProduct
    {
        public async Task<Reponse> CreateAsync(Product entity)
        {
            try 
            {
                var getProduct  = await GetByAsync(_ => _.Equals(entity) );
                if (getProduct is not null && !string.IsNullOrEmpty(getProduct.Name))
                {
                    return new Reponse(false, $"{getProduct.Name} already added");
                }
                var product = new Product() 
                {
                 Name = entity.Name,
                 Price = entity.Price,
                 Quanlity = entity.Quanlity,
                };
                var current = context.Products.Add(product).Entity;
                await context.SaveChangesAsync();
                if (current is not null)
                    return new Reponse(true, $"{current} added to database successfully");
                else return new Reponse(false, $"Error occurred while adding {entity.Name}");

            }
            catch(Exception ex) 
            {
                LogException.LogExceptions(ex);
                return new Reponse(false, "Error occurred adding new product");
            }
        }

        public async Task<Reponse> DeleteAsync(int id)
        {
            try
            {
                var productId = await FindByIdAsync(id);
                if (productId is null) return new Reponse(false, $"{productId.Id} not found");
                context.Products.Remove(productId);
                await context.SaveChangesAsync();
                return new Reponse(true, $"{productId.Id} deleted successfully");

            }
            catch (Exception ex) 
            {
                LogException.LogExceptions(ex);
                return new Reponse(false, "Error occurred deleting product");
            }
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            var productId = await context.Products.Where(_ => _.Id.Equals(id)).FirstOrDefaultAsync();
            return productId is not null ? productId : null!;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetByAsync(Expression<Func<Product, bool>> predicate)
        {
            var product = await context.Products.Where(predicate).FirstOrDefaultAsync();
            return product is not null ? product : null!; 
        }

        public async Task<Reponse> UpdateAsync(Product entity)
        {
            try 
            {
                var product = await FindByIdAsync(entity.Id);
                if (product is null) return new Reponse(false, $"{entity.Id} not found");
                product.Name = entity.Name;
                product.Price = entity.Price;
                product.Quanlity = entity.Quanlity;
                await context.SaveChangesAsync();
                return new Reponse(true, $"updated successfully");
            }
            catch(Exception ex) 
            {
                LogException.LogExceptions(ex);
                return new Reponse(false, "Error occurred Updating ");
            }
        }
    }
}
