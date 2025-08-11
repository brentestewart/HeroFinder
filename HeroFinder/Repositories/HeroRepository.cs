using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using HeroFinder.Shared.DTOs;
using HeroFinder.Shared.Repositories;

namespace HeroFinder.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly string _jsonPath;
        private List<HeroDto>? _heroes;
        private readonly object _lock = new();

        public HeroRepository()
        {
            _jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "heroes.json");
        }

        public IEnumerable<HeroDto> GetHeroes()
        {
            if (_heroes == null)
            {
                lock (_lock)
                {
                    if (_heroes == null)
                    {
                        var json = File.ReadAllText(_jsonPath);
                        _heroes = JsonSerializer.Deserialize<List<HeroDto>>(json) ?? new List<HeroDto>();
                    }
                }
            }
            return _heroes;
        }

        public HeroDto? GetHeroById(int id)
        {
            return GetHeroes().FirstOrDefault(h => h.Id == id);
        }

        public IEnumerable<HeroDto> GetFavoriteHeroes()
        {
            return GetHeroes().Where(h => h.IsFavorite);
        }

        public Task<bool> UpdateHeroFavoriteAsync(int id, bool isFavorite)
        {
            try
            {
                lock (_lock)
                {
                    // Reload the latest data from file
                    var json = File.ReadAllText(_jsonPath);
                    _heroes = JsonSerializer.Deserialize<List<HeroDto>>(json) ?? new List<HeroDto>();
                    
                    var hero = _heroes.FirstOrDefault(h => h.Id == id);
                    if (hero == null)
                        return Task.FromResult(false);

                    hero.IsFavorite = isFavorite;

                    // Save back to file
                    var options = new JsonSerializerOptions 
                    { 
                        WriteIndented = true 
                    };
                    var updatedJson = JsonSerializer.Serialize(_heroes, options);
                    File.WriteAllText(_jsonPath, updatedJson);
                }

                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }
}
