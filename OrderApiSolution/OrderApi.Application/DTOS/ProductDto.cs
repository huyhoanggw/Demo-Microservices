using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.DTOS
{
   public record ProductDto
        (
         int Id,
         [Required] string Name,
         [Required , Range(1,int.MaxValue)] int Quanlity ,
         [Required , DataType(DataType.Currency)] decimal Price
         
       );
}
