using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServicesManager servicesManager) : ControllerBase
    {
        // Login
        [HttpPost("login")] // POST: /api/auth/login 
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await servicesManager.AuthService.LoginAsync(loginDto);
            return Ok(result);
        }

        //Register
        [HttpPost("register")] // POST: /api/auth/register 
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await servicesManager.AuthService.RegisterAsync(registerDto);
            return Ok(result);
        }
    }
}
