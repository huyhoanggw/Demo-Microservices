using AuthenticationApi.Application.Dto;
using AuthenticationApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthenticationController(IUser userRepository): Controller
    {
        [HttpPost("register")] 
        public async Task<IActionResult >  Register(AppUserDto request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var result = await userRepository.Register(request);

            return result.Flag ? Ok(result) : BadRequest();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto requst)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await userRepository.Login(requst);
            return result.Flag ? Ok(result) : NotFound();
        }
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id) 
        {
            var result = await userRepository.GetUser(id);
            return result is not null ? Ok(result) : NotFound();
        } 
    }
}
