using GitHubIntegration.CachedServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubIntegration
{
    public static class Extensions
    {
        public static void AddGitHubIntegration(this IServiceCollection Services, Action<GitHubIntegrationOptions> configOptions)
        {
            Services.Configure(configOptions);
            Services.AddScoped<IGitHubService, GitHubService>();
            Services.Decorate<IGitHubService, CachedGitHubServices>();
        }
    }
}
