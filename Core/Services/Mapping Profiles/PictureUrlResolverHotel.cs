using AutoMapper;
using Domain.Entites;
using Microsoft.Extensions.Configuration;
using Shared.DTOs;

namespace Services.Mapping_Profiles
{
    internal class PictureUrlResolverHotel(IConfiguration configuration) : IValueResolver<Hotel, HotelReturnDTO, string>
    {
        public string Resolve(Hotel source, HotelReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.PictureUrl))
                return string.Empty;

            return $"{configuration["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
