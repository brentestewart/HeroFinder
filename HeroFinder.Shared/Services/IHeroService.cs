using System.Collections.Generic;
using HeroFinder.Shared.DTOs;

namespace HeroFinder.Shared.Services
{
    public interface IHeroService
    {
        IEnumerable<HeroDto> GetHeroes();
        HeroDto? GetHeroById(int id);
        Task<bool> UpdateHeroFavoriteAsync(int id, bool isFavorite);
        IEnumerable<HeroDto> GetFavoriteHeroes();
    }
}
