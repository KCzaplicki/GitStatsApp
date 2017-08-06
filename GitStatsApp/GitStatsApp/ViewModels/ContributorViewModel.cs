using GalaSoft.MvvmLight;
using GitStatsApp.Consts;
using GitStatsApp.Dtos;
using GitStatsApp.Services;
using System;
using System.Threading.Tasks;

namespace GitStatsApp.ViewModels
{
    public class ContributorViewModel : ViewModelBase
    {
        public string Title { get; } = AppConsts.AppName;
        public string ContributorIcon { get; } = "user.png";
        public string DateIcon { get; } = "date.png";

        public RepositoryDto Repository
        {
            get
            {
                return repository;
            }
            set
            {
                repository = value;
                RaisePropertyChanged(() => Repository);
            }
        }
        public ContributorDto Contributor
        {
            get
            {
                return contributor;
            }
            set
            {
                contributor = value;
                RaisePropertyChanged(() => Contributor);
            }
        }
        public ContributorStatsDto ContributorStats
        {
            get
            {
                return contributorStats;
            }
            set
            {
                contributorStats = value;
                RaisePropertyChanged(() => ContributorStats);
            }
        }
        public string ContributorStatsDateRange
        {
            get
            {
                return contributorStatsDateRange;
            }
            set
            {
                contributorStatsDateRange = value;
                RaisePropertyChanged(() => ContributorStatsDateRange);
            }
        }

        private RepositoryDto repository;
        private ContributorDto contributor;
        private ContributorStatsDto contributorStats;
        private string contributorStatsDateRange;
        private IContributorService _contributorService;

        public ContributorViewModel(IContributorService contributorService)
        {
            _contributorService = contributorService;

            var contributorStatsDateFrom = (DateTimeConsts.UnixEpoch).ToString(DateTimeConsts.DateTimeFormat);
            var contributorStatsDateTo = (DateTime.Today).ToString(DateTimeConsts.DateTimeFormat);
            ContributorStatsDateRange = $"{contributorStatsDateFrom} - {contributorStatsDateTo}";
        }

        public async Task LoadContributorStats(string contributorId)
        {
            ContributorStats = new ContributorStatsDto();
            ContributorStats = await _contributorService.GetContributorStats(contributorId);
        }
    }
}
