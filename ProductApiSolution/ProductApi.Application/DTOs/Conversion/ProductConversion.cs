using ProductApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.DTOs.Conversion
{
    public class ProductConversion
    {
        public static Product ToEntity(ProductDTO dto) => new Product() { Id = dto.Id, Name = dto.Name, Price = dto.Price, Quanlity = dto.Quanlity };
        public static (ProductDTO?, IEnumerable<ProductDTO>?) FromEntity(Product? product, IEnumerable<Product>? products)
        {
            if (product is not null || products is null)
                return (new ProductDTO(product.Id , product.Name , product.Quanlity , product.Price), null);
            if (product is null || products is not null)
                return (null, products.Select( p =>  new ProductDTO(p.Id, p.Name, p.Quanlity, p.Price)).ToList());
            return (null, null);
        }
    }
}
