using GitHubIntegration;
using GitHubIntegration.DataEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using System.Runtime.CompilerServices;

namespace GitHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubAPIController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;
        public GitHubAPIController(IGitHubService gitHubService) { _gitHubService = gitHubService; }
        
        [HttpGet("/portfolio")]
        public async Task<IActionResult> GetPortfolio()
        {
            Portfolio res = await _gitHubService.GetPortfolio();
            if (res == null)
            {
                return NoContent();
            }
            else return Ok(Portfolio.Repositories);
        }

        [HttpGet("/search")]
        public async Task<IActionResult> SearchRepositories(string? repoName, Language? language, string? userName)
        {
            var res = await _gitHubService.SearchRepositoriesAsync(repoName, language, userName);
            if (res.Count == 0)
            {
                return NoContent();
            }
            return Ok(res);
        }
    }
}
