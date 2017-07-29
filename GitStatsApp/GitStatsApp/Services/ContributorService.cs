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
        public async Task<ContibutorStatsDto> GetContributorStats(string contributorId, DateTime? from = null, DateTime? to = null)
        {
            var client = new HttpClient();

            var toValue = from.HasValue && !to.HasValue ? DateTime.Now : to.Value;
            var response = await client.GetAsync(from.HasValue ?
                string.Format(RestApiUrls.GetContributorStatsWithRangeUrl, contributorId, from.Value.ToUnixTimestamp(), toValue.ToUnixTimestamp()) :
                string.Format(RestApiUrls.GetContributorStatsUrl, contributorId));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ContibutorStatsDto>(content);
            }

            return null;
        }
    }
}
