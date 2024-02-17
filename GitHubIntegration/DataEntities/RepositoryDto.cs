using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubIntegration.DataEntities
{
    public class RepositoryDto
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public string UserName { get; set; }

        public RepositoryDto(string name, string language, string userName)
        {
            Name = name;
            Language = language;    
            UserName = userName;
        }
    }
}
