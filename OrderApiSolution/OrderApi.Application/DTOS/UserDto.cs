using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.DTOS
{
    public record UserDto
        (
             int Id ,
             [Required] string UserName ,
             [Required] string TelePhone ,
             [Required] string Address,
             [Required , EmailAddress] string Email ,
             [Required] string Password ,
             [Required] string Role 
        );
}
