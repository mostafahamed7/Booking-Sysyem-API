using Domain.Entites.Reservations_Mod;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entites.Identity
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }

    }
}
