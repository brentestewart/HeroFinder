using System.Collections.Generic;
using System.Linq;
using HeroFinder.Shared.DTOs;
using HeroFinder.Shared.Services;
using HeroFinder.Shared.Repositories;

namespace HeroFinder.Services
{
    public class HeroService : IHeroService
    {
        private readonly IHeroRepository _heroRepository;

        public HeroService(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public IEnumerable<HeroDto> GetHeroes()
        {
            return _heroRepository.GetHeroes().OrderBy(h => h.HeroName);
        }

        public HeroDto? GetHeroById(int id)
        {
            return _heroRepository.GetHeroById(id);
        }

        public IEnumerable<HeroDto> GetFavoriteHeroes()
        {
            return _heroRepository.GetFavoriteHeroes().OrderBy(h => h.HeroName);
        }

        public async Task<bool> UpdateHeroFavoriteAsync(int id, bool isFavorite)
        {
            return await _heroRepository.UpdateHeroFavoriteAsync(id, isFavorite);
        }
    }
}

