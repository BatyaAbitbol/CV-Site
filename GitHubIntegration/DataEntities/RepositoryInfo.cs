using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubIntegration.DataEntities;

public class RepositoryInfo
{
    private readonly Repository _repository;
    private readonly GitHubClient _client;
    public string Name { get; set; }
    public IReadOnlyList<RepositoryLanguage> Languages { get; set; }
    public DateTimeOffset LastCommit { get; set; }
    public int StarsCount { get; set; }
    public int PullRequestCount { get; set; }
    public string? Url { get; set; }

    public RepositoryInfo(Repository repository, GitHubClient client)
    {
        _repository = repository;
        _client = client;
    }
    public async Task Initialize()
    {
        Name = _repository.Name;
        Languages = await _client.Repository.GetAllLanguages(_repository.Id);
        LastCommit = _repository.UpdatedAt;
        StarsCount = _repository.StargazersCount;
        var pullRequest = await _client.PullRequest.GetAllForRepository(_repository.Id);
        PullRequestCount = pullRequest.Count;
        Url = _repository.Url;
    }
}
