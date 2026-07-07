using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Application.Dto
{
    public record class LoginDto([Required] string Email , [Required] string Password);
    
    
}
