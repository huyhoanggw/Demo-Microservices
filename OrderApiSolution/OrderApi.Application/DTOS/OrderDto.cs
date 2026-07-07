using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.DTOS
{
    public record OrderDto
        (
        int Id ,
        [Required , Range(1, int.MaxValue )] int ProductId , 
        [Required , Range(1, int.MaxValue )] int ClientId, 
        [Required , Range(1, int.MaxValue )] int PurchaseQuanlity ,
        DateTime OrderedDate 

        );
}
