using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public record RegisterDto
    {
        [Required(ErrorMessage = "DisplayName is Required")]
        public string DisplayName { get; init; }

        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; init; }

        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; init; }
    }
}
