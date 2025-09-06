using AutoMapper;
using Domain.Entites.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared;
using Shared.DTOs;
using Shared.DTOs.Order_Module;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    internal class AuthenticationServices(UserManager<User> _userManager,
        IOptions<JwtOptions> options,
        IMapper mapper
        ) : IAuthenticationServices
    {
        public async Task<bool> CheckEmailExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            // Check on Emil & Password
            // 1. Check If there is user under this Email
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                throw new UnAuthorizedException($"Email {loginDto.Email} does not exist");

            // 2. Check If Password is correct
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
                throw new UnAuthorizedException();

            // Generate Token & Response
            return new UserResultDto(
                user.DisplayName,
                user.Email,
                await CreateTokenAsync(user)
                );
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new Domain.Exceptions.ValidationException(errors);
            }
            // Generate Token & Response
            return new UserResultDto(
                user.DisplayName,
                user.Email,
                await CreateTokenAsync(user)
                );
        }
        public async Task<AddressDTO> GetUserAddress(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                                               .FirstOrDefaultAsync(u => u.Email == email)
                                               ?? throw new UserNotFoundException(email);
            return mapper.Map<AddressDTO>(user.Address);

        }

        public async Task<UserResultDto> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new UserNotFoundException(email);
            return new UserResultDto(
                user.DisplayName,
                user.Email,
                await CreateTokenAsync(user)
            );
        }

        public async Task<AddressDTO> UpdateUserAddress(AddressDTO addressDto, string email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                                               .FirstOrDefaultAsync(u => u.Email == email)
                                               ?? throw new UserNotFoundException(email);
            // User Address is null, Create New Address
            // User Address is not null, Update Existing Address
            if (user.Address != null)
            {
                user.Address.FristName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.Street = addressDto.Street;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
            }
            else
            {
                var userAdderss = mapper.Map<Address>(addressDto);
                user.Address = userAdderss;
            }
            await _userManager.UpdateAsync(user);
            return mapper.Map<AddressDTO>(user.Address);
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var JwtOptions = options.Value;
            // Create Claims
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.DisplayName)
            };
            // Add Roles To Claims
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            // Create Secret Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));

            // Create Algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create Token
            var token = new JwtSecurityToken(
                issuer: JwtOptions.Issuer,
                audience: JwtOptions.Audience,
                claims: authClaims,
                expires: DateTime.Now.AddDays(JwtOptions.DiurationInDays),
                signingCredentials: creds
            );
            // Create Object from JwtSecurityTokenHandler
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
