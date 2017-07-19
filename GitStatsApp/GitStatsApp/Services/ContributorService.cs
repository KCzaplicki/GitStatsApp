using System;
using System.Threading.Tasks;
using GitStatsApp.Dtos;
using GitStatsApp.Enums;
using System.Net.Http;
using Newtonsoft.Json;

namespace GitStatsApp.Services
{
    public class ContributorService : IContributorService
    {
        public async Task<ContibutorStatsDto> GetContributorStats(int contributorId, DateTime? from = null, DateTime? to = null)
        {
            const string dateFormat = "dd-MM-yyyy";
            var client = new HttpClient();
            var response = await client.GetAsync(from.HasValue && to.HasValue ?
                string.Format(RestApiUrls.GetContributorStatsWithRangeUrl, contributorId, from.Value.ToString(dateFormat), to.Value.ToString(dateFormat)) :
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
