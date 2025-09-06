using Shared.DTOs;
using Shared.DTOs.Order_Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAuthenticationServices
    {
        public Task<UserResultDto> LoginAsync(LoginDto loginDto);
        public Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        public Task<bool> CheckEmailExists(string email);

        // Get Current User
        public Task<UserResultDto> GetUserByEmail(string email);
        // Get User Address
        public Task<AddressDTO> GetUserAddress(string email);

        // Upate User Address
        public Task<AddressDTO> UpdateUserAddress(AddressDTO addressDto, string email);

    }
}
