using System.Collections.Generic;
using GitStatsApp.Dtos;
using GitStatsApp.Enums;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace GitStatsApp.Services
{
    public class RepositoryService : IRepositoryService
    {
        private HttpClient _httpClient;

        public RepositoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<RepositoryDto>> GetRepositories()
        {
            var response = await _httpClient.GetAsync(RestApiUrls.GetRepositoriesUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RepositoryDto>>(content);
            }

            return null;
        }

        public async Task<IList<ContributorDto>> GetRepositoryContributors(string repositoryId)
        {
            var response = await _httpClient.GetAsync(string.Format(RestApiUrls.GetRepositoryContributorsUrl, repositoryId));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ContributorDto>>(content);
            }

            return null;
        }
    }
}
