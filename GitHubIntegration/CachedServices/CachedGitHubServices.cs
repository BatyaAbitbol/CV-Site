using GitHubIntegration.DataEntities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Octokit;
using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubIntegration.CachedServices
{
    public class CachedGitHubServices : IGitHubService
    {
        private readonly IGitHubService _gitHubService;
        private readonly IMemoryCache _memoryCache;
        private readonly GitHubIntegrationOptions _options;

        private const string UserPortfolioKey = "UserPortfolioKey";
        public CachedGitHubServices(IGitHubService gitHubService, IMemoryCache memoryCache, IOptions<GitHubIntegrationOptions> options)
        {
            _gitHubService = gitHubService;
            _memoryCache = memoryCache;
            _options = options.Value;
        }
        public async Task<Portfolio> GetPortfolio()
        {
            if (_memoryCache.TryGetValue(UserPortfolioKey, out Portfolio portfolio))
                return portfolio;

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(30))
                .SetSlidingExpiration(TimeSpan.FromSeconds(10)); 

            portfolio = await _gitHubService.GetPortfolio();
            _memoryCache.Set(UserPortfolioKey, portfolio, cacheOptions);
            return portfolio;
        }

        public async Task<List<RepositoryDto>> SearchRepositoriesAsync(string? repoName, Language? language, string? userName)
        {
            return await _gitHubService.SearchRepositoriesAsync(repoName, language, userName);
        }
    }
}
