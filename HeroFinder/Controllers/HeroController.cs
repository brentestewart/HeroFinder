using HeroFinder.Services;
using HeroFinder.Shared.DTOs;
using HeroFinder.Shared.Services;
using HeroFinder.Shared.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HeroFinder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroController : ControllerBase
    {
        private readonly IHeroService _heroService;

        public HeroController(IHeroService heroService)
        {
            _heroService = heroService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<HeroDto>> GetHeroes()
        {
            return Ok(_heroService.GetHeroes());
        }

        [HttpGet("{id}")]
        public ActionResult<HeroDto> GetHeroById(int id)
        {
            var hero = _heroService.GetHeroById(id);
            if (hero == null) return NotFound();
            return Ok(hero);
        }

        [HttpGet("favorites")]
        public ActionResult<IEnumerable<HeroDto>> GetFavoriteHeroes()
        {
            return Ok(_heroService.GetFavoriteHeroes());
        }

        [HttpPut("{id}/favorite")]
        public async Task<ActionResult> UpdateHeroFavorite(int id, [FromBody] bool isFavorite)
        {
            var success = await _heroService.UpdateHeroFavoriteAsync(id, isFavorite);
            if (!success) return NotFound();
            return Ok();
        }
    }
}
