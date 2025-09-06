using AutoMapper;
using Domain.Entites;
using Microsoft.Extensions.Configuration;
using Shared.DTOs;

namespace Services.Mapping_Profiles
{
    internal class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Room, RoomReturnDTO, string>
    {
        public string Resolve(Room source, RoomReturnDTO destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrWhiteSpace(source.PictureUrl))
                return string.Empty;

            return $"{configuration["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
