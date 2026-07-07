using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.SharedLibary.Reponse
{
    public record Reponse(bool Flag = false , string message = null!);
    
}
