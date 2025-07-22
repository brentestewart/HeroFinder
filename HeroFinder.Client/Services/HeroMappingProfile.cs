using AutoMapper;
using HeroFinder.Shared.DTOs;
using HeroFinder.Shared.Models;

namespace HeroFinder.Client.Services
{
    public class HeroMappingProfile : Profile
    {
        public HeroMappingProfile()
        {
            CreateMap<HeroDto, Hero>();
        }
    }
}
