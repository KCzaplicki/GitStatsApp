using System;
using System.Threading.Tasks;
using GitStatsApp.Dtos;
using GitStatsApp.Enums;
using System.Net.Http;
using Newtonsoft.Json;
using GitStatsApp.Helpers;

namespace GitStatsApp.Services
{
    public class ContributorService : IContributorService
    {
        private HttpClient _httpClient;

        public ContributorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ContributorStatsDto> GetContributorStats(string contributorId, DateTime? from = null, DateTime? to = null)
        {
            DateTime toValue = DateTime.Now;

            if (from.HasValue && to.HasValue)
            {
                toValue = to.Value;
            }

            var response = await _httpClient.GetAsync(from.HasValue ?
                string.Format(RestApiUrls.GetContributorStatsWithRangeUrl, contributorId, from.Value.ToUnixTimestamp(), toValue.ToUnixTimestamp()) :
                string.Format(RestApiUrls.GetContributorStatsUrl, contributorId));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ContributorStatsDto>(content);
            }

            return null;
        }
    }
}
