using Clothes.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequestDto registerRequestDto) 
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Email
            };
           var result= await userManager.CreateAsync(identityUser,registerRequestDto.Password);
            if (result.Succeeded)
            {
                if(registerRequestDto.Roles!=null && registerRequestDto.Roles.Any())
                {
                     result=await userManager.AddToRolesAsync(identityUser,registerRequestDto.Roles);
                    if (result.Succeeded)
                    {
                        return Ok("User Register Successfully");
                    }
                }
                
            }
            return BadRequest("Error Happened");
        }


        [HttpPost]
        [Route("Login/User")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Email);
            if (user != null)
            {
                var checkPass = await userManager.CheckPasswordAsync(user, loginRequestDto.password);
                if (checkPass)
                {
                    // Check the user's role
                    var userRoles = await userManager.GetRolesAsync(user);
                    if (userRoles.Contains("Reader"))
                    {
                        return Ok(new { UserId = user.Id, Role = "User" });
                    }
                    
                }
            }
            return BadRequest();
        }




        [HttpPost]
        [Route("Login/Admin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Email);
            if (user != null)
            {
                var checkPass = await userManager.CheckPasswordAsync(user, loginRequestDto.password);
                if (checkPass)
                {
                    var userRoles = await userManager.GetRolesAsync(user);
                    if (userRoles.Contains("Writer"))
                    {
                        return Ok(new { UserId = user.Id, Role = "Admin" });
                    }
                }
            }
            return BadRequest();
        }
    }
}
