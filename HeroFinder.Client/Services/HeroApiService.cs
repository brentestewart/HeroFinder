using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using HeroFinder.Shared.DTOs;
using HeroFinder.Shared.Models;
using AutoMapper;

namespace HeroFinder.Client.Services
{
    public class HeroApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public HeroApiService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<List<Hero>> GetHeroesAsync()
        {
            var dtos = await _httpClient.GetFromJsonAsync<List<HeroDto>>("api/hero") ?? new List<HeroDto>();
            return _mapper.Map<List<Hero>>(dtos);
        }

        public async Task<Hero?> GetHeroByIdAsync(int id)
        {
            var dto = await _httpClient.GetFromJsonAsync<HeroDto>($"api/hero/{id}");
            return dto == null ? null : _mapper.Map<Hero>(dto);
        }

        public async Task<List<Hero>> GetFavoriteHeroesAsync()
        {
            var dtos = await _httpClient.GetFromJsonAsync<List<HeroDto>>("api/hero/favorites") ?? new List<HeroDto>();
            return _mapper.Map<List<Hero>>(dtos);
        }

        public async Task<bool> UpdateHeroFavoriteAsync(int id, bool isFavorite)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/hero/{id}/favorite", isFavorite);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
