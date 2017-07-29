using GitStatsApp.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitStatsApp.Services
{
    public interface IRepositoryService
    {
        Task<IList<RepositoryDto>> GetRepositories();
        Task<IList<ContributorDto>> GetRepositoryContributors(string repositoryId);
    }
}
