using System.Collections.Generic;
using HeroFinder.Shared.DTOs;

namespace HeroFinder.Shared.Repositories
{
    public interface IHeroRepository
    {
        IEnumerable<HeroDto> GetHeroes();
        HeroDto? GetHeroById(int id);
        Task<bool> UpdateHeroFavoriteAsync(int id, bool isFavorite);
        IEnumerable<HeroDto> GetFavoriteHeroes();
    }
}
