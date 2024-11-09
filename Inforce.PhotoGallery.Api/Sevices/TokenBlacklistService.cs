using Inforce.PhotoGallery.Api.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Inforce.PhotoGallery.Api.Sevices
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _tokenExpiryTime = TimeSpan.FromHours(1);

        public TokenBlacklistService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void BlacklistToken(string token)
        {
            _memoryCache.Set(token, true, _tokenExpiryTime);
        }

        public bool IsTokenBlacklisted(string token)
        {
            return _memoryCache.TryGetValue(token, out _);
        }
    }
}
