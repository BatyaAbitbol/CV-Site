using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubIntegration.DataEntities
{
    public class Portfolio
    {
        private static Portfolio _portfolio = new Portfolio();
        public static List<RepositoryInfo> Repositories { get; set; }
        private Portfolio() 
        { 
            Repositories = new List<RepositoryInfo>();
        }
        public static async Task<Portfolio> GetPortfolioAsync(GitHubClient gitHubClient)
        {
            var client = await gitHubClient.User.Current();
            var repositories = await gitHubClient.Repository.GetAllForUser(client.Login);
            foreach (var repo in repositories)
            {
                RepositoryInfo repoInfo = new RepositoryInfo(repo, gitHubClient);
                await repoInfo.Initialize();
                Repositories.Add(repoInfo);
            }
            return _portfolio;
        }
    }
}