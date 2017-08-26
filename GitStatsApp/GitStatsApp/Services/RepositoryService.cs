using System.Collections.Generic;
using GitStatsApp.Dtos;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using GitStatsApp.Consts;

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
            var response = await _httpClient.GetAsync(RestApiUrlsConsts.GetRepositoriesUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RepositoryDto>>(content);
            }

            return null;
        }

        public async Task<IList<ContributorDto>> GetRepositoryContributors(string repositoryId)
        {
            var response = await _httpClient.GetAsync(string.Format(RestApiUrlsConsts.GetRepositoryContributorsUrl, repositoryId));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ContributorDto>>(content);
            }

            return null;
        }
    }
}
