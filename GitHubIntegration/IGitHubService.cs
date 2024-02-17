using GitHubIntegration.DataEntities;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubIntegration
{
    public interface IGitHubService
    {
        public Task<Portfolio> GetPortfolio();
        public Task<List<RepositoryDto>> SearchRepositoriesAsync(string? repoName, Language? language, string? userName);
    }
}
