using System.Collections.Generic;

namespace HeroFinder.Shared.DTOs
{
    public class HeroDto
    {
        public int Id { get; set; }
        public string HeroName { get; set; } = string.Empty;
        public string SecretIdentity { get; set; } = string.Empty;
        public List<string> Abilities { get; set; } = new();
        public string ImageLink { get; set; } = string.Empty;
        public string Universe { get; set; } = string.Empty;
        public bool IsFavorite { get; set; }
    }
}
