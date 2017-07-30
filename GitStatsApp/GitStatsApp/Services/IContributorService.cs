using GitStatsApp.Dtos;
using System;
using System.Threading.Tasks;

namespace GitStatsApp.Services
{
    public interface IContributorService
    {
        Task<ContributorStatsDto> GetContributorStats(string contributorId, DateTime? from = null, DateTime? to = null);
    }
}
