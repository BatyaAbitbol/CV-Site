using GitHubIntegration.DataEntities;
using Microsoft.Extensions.Options;
using Octokit;
using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GitHubIntegration
{
    public class GitHubService : IGitHubService
    {
        private readonly GitHubClient _client;
        private readonly GitHubIntegrationOptions _options;
        private readonly InMemoryCredentialStore _credentials;

        public GitHubService(IOptions<GitHubIntegrationOptions> options)
        {
            _options = options.Value;
            _credentials = new InMemoryCredentialStore(new Credentials(_options.GitHubToken));
            _client = new GitHubClient(new ProductHeaderValue("my-github-app"), _credentials);
        }
        public async Task<Portfolio> GetPortfolio()
        {
            return await Portfolio.GetPortfolioAsync(_client);
        }

        public async Task<List<RepositoryDto>> SearchRepositoriesAsync(string? repoName, Language? language, string? userName)
        {
            var request = new SearchRepositoriesRequest();
            if (repoName != null)
                request = new SearchRepositoriesRequest(repoName);

            if (userName != null)
                request.User = userName;

            if (language != null)
                request.Language = language;

            var result = await _client.Search.SearchRepo(request);
            var response = new List<RepositoryDto>();
            foreach (var repo in result.Items)
            {
                response.Add(new RepositoryDto(repo.Name, repo.Language, repo.Owner.Login));
            }
            return response;
        }
    }
}
