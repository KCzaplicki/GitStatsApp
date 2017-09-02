using System;
using System.Threading.Tasks;
using GitStatsApp.Dtos;
using System.Net.Http;
using Newtonsoft.Json;
using GitStatsApp.Helpers;
using GitStatsApp.Consts;

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
            DateTime toValue = DateTimeConsts.Today;

            if (from.HasValue && to.HasValue)
            {
                toValue = to.Value;
            }

            var response = await _httpClient.GetAsync(from.HasValue ?
                string.Format(RestApiUrlsConsts.GetContributorStatsWithRangeUrl, contributorId, from.Value.ToUnixTimestamp(), toValue.ToUnixTimestamp()) :
                string.Format(RestApiUrlsConsts.GetContributorStatsUrl, contributorId));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ContributorStatsDto>(content);
            }

            return null;
        }
    }
}
