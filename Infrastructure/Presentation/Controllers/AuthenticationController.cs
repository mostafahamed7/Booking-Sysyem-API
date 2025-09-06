using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOs;
using Shared.DTOs.Order_Module;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManger serviceManger) : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
            => Ok(await serviceManger.AuthenticationServices.LoginAsync(loginDto));

        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
            => Ok(await serviceManger.AuthenticationServices.RegisterAsync(registerDto));


        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return Ok(await serviceManger.AuthenticationServices.CheckEmailExists(email));
        }

        // Get Current User 
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDto>> GetUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManger.AuthenticationServices.GetUserByEmail(email);

            return Ok(result);
        }

        // Get User Address
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManger.AuthenticationServices.GetUserAddress(email);

            return Ok(result);
        }

        // Update User Address
        [Authorize]
        [HttpPut("updateAddress")]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManger.AuthenticationServices.UpdateUserAddress(address, email);

            return Ok(result);
        }
    }
}
