using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Application.Dto
{
    public record class AppUserDto(
               [Required] string Name ,
        [Required] string TelePhone ,
        [Required] string Address,
        [Required] string Email,
        [Required] string Role ,
        [Required] string Password
        );
    
    
}
