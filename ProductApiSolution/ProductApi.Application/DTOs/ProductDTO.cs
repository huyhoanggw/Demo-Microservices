
using System.ComponentModel.DataAnnotations;


namespace ProductApi.Application.DTOs
{
    public record ProductDTO
        (
        int Id ,
        [Required] string Name ,
        [Required,Range(1,int.MaxValue)] int Quanlity ,
        [Required , DataType(DataType.Currency)] decimal Price
        );
    
}
