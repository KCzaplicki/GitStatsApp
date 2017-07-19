using System.Collections.Generic;
using GitStatsApp.Dtos;
using GitStatsApp.Enums;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;

namespace GitStatsApp.Services
{
    public class RepositoryService : IRepositoryService
    {
        public async Task<IList<RepositoryDto>> GetRepositories()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(RestApiUrls.GetRepositoriesUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RepositoryDto>>(content);
            }

            return null;
        }

        public async Task<IList<ContributorDto>> GetRepositoryContributors(int repositoryId)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(string.Format(RestApiUrls.GetRepositoryContributorsUrl, repositoryId));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ContributorDto>>(content);
            }

            return null;
        }
    }
}
