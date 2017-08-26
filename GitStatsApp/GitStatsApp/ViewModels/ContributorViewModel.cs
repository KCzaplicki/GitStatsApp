using GalaSoft.MvvmLight;
using GitStatsApp.Consts;
using GitStatsApp.Dtos;
using GitStatsApp.Helpers;
using GitStatsApp.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GitStatsApp.ViewModels
{
    public class ContributorViewModel : ViewModelBase
    {
        public string Title { get; } = AppConsts.AppName;
        public string ContributorIcon { get; } = "user.png";
        public string DateIcon { get; } = "date.png";
        public string HamburgerIcon { get; } = "hamburger.png";
        public string MergersHeader { get; } = "Merges";
        public string CommitsHeader { get; } = "Commits";
        public string LinesOfCodeHeader { get; } = "Lines of code";
        public string ContribToProjectHeader { get; } = "Contribution to the project";
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
        public ContributorStatsDto ContributorStatsIncrement
        {
            get
            {
                return contributorStatsIncrement;
            }
            set
            {
                contributorStatsIncrement = value;
                RaisePropertyChanged(() => ContributorStatsIncrement);
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
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }
        public ICommand ChangeSelectDateRangeCommand { get; private set; }

        private bool isLoading;
        private RepositoryDto repository;
        private ContributorDto contributor;
        private ContributorStatsDto contributorStats;
        private ContributorStatsDto contributorStatsIncrement;
        private string contributorStatsDateRange;
        private IContributorService _contributorService;
        
        private const string ContributorStatsDateRangeStringFormat = "{0} - {1}";

        public ContributorViewModel(IContributorService contributorService)
        {
            _contributorService = contributorService;

            var contributorStatsDateFrom = (DateTimeConsts.UnixEpoch).ToString(DateTimeConsts.DateTimeFormat);
            var contributorStatsDateTo = (DateTime.Today).ToString(DateTimeConsts.DateTimeFormat);
            ContributorStatsDateRange = string.Format(ContributorStatsDateRangeStringFormat, contributorStatsDateFrom, contributorStatsDateTo);

            ChangeSelectDateRangeCommand = new Command(async () => await ChangeDateRage());
        }

        public async Task ChangeDateRage()
        {
            if (IsLoading)
            {
                return;
            }

            const string changeDateRangeHeader = "Select date range";
            const string changeDateRangeCancel = "Cancel";

            var page = App.Current.MainPage;
            var selectedDateRange = await page.DisplayActionSheet(changeDateRangeHeader, changeDateRangeCancel, null, DateRangesConsts.DateRanges.ToArray());

            if (!DateRangesConsts.DateRanges.Contains(selectedDateRange))
            {
                return;
            }

            await LoadContributorStats(Contributor.Id, selectedDateRange);
        }

        public async Task LoadContributorStats(string contributorId, string dateRange = DateRangesConsts.All)
        {
            IsLoading = true;

            DateTime contributorStatsDateFrom = DateRangeHelper.GetStartRangeDateTime(dateRange);
            var contributorStatsDateToString = (DateTime.Today).ToString(DateTimeConsts.DateTimeFormat);
            var contributorStatsDateFromString = (contributorStatsDateFrom).ToString(DateTimeConsts.DateTimeFormat);
            ContributorStatsDateRange = string.Format(ContributorStatsDateRangeStringFormat, contributorStatsDateFromString, contributorStatsDateToString);

            ContributorStats = await _contributorService.GetContributorStats(contributorId, contributorStatsDateFrom, DateTime.UtcNow);

            if (dateRange == DateRangesConsts.All)
            {
                var previousContributorStats = await _contributorService.GetContributorStats(contributorId, DateTimeConsts.UnixEpoch, DateTimeConsts.Yesterday);

                ContributorStatsIncrement = new ContributorStatsDto
                {
                    Commits = contributorStats.Commits - previousContributorStats.Commits,
                    Merges = contributorStats.Merges - previousContributorStats.Merges,
                    LinesOfCode = contributorStats.LinesOfCode - previousContributorStats.LinesOfCode,
                    ContribToProject = contributorStats.ContribToProject - previousContributorStats.ContribToProject
                };
            }
            else
            {
                ContributorStatsIncrement = new ContributorStatsDto
                {
                    Commits = int.MinValue,
                    Merges = int.MinValue,
                    LinesOfCode = int.MinValue,
                    ContribToProject = double.MinValue
                };
            }

            IsLoading = false;
        }
    }
}
