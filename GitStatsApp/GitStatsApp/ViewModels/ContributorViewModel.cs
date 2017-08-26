using GalaSoft.MvvmLight;
using GitStatsApp.Consts;
using GitStatsApp.Dtos;
using GitStatsApp.Enums;
using GitStatsApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public ContributorViewModel(IContributorService contributorService)
        {
            _contributorService = contributorService;

            var contributorStatsDateFrom = (DateTimeConsts.UnixEpoch).ToString(DateTimeConsts.DateTimeFormat);
            var contributorStatsDateTo = (DateTime.Today).ToString(DateTimeConsts.DateTimeFormat);
            ContributorStatsDateRange = $"{contributorStatsDateFrom} - {contributorStatsDateTo}";
            ChangeSelectDateRangeCommand = new Command(async () => await ChangeDateRage());

        }

        public async Task ChangeDateRage()
        {
            if (IsLoading)
            {
                return;
            }

            var dateRanges = new List<string> {
                DateRanges.Day,
                DateRanges.Week,
                DateRanges.Month,
                DateRanges.Year,
                DateRanges.All
            };
            var selectedDateRange = await App.Current.MainPage.DisplayActionSheet("Select date range", "Cancel", null, dateRanges.ToArray());

            if (!dateRanges.Contains(selectedDateRange))
            {
                IsLoading = false;
                return;
            }

            await LoadContributorStats(ContributorStats.Id, selectedDateRange);
        }

        public async Task LoadContributorStats(string contributorId, string dateRange = DateRanges.All)
        {
            IsLoading = true;

            ContributorStats = new ContributorStatsDto();
            ContributorStatsIncrement = new ContributorStatsDto();

            DateTime contributorStatsDateFrom = DateTimeConsts.UnixEpoch;
            var contributorStatsDateToString = (DateTime.Today).ToString(DateTimeConsts.DateTimeFormat);

            switch (dateRange)
            {
                case DateRanges.Day:
                    contributorStatsDateFrom = DateTimeConsts.Yesterday;
                    break;
                case DateRanges.Week:
                    contributorStatsDateFrom = DateTimeConsts.LastWeek;
                    break;
                case DateRanges.Month:
                    contributorStatsDateFrom = DateTimeConsts.LastMonth;
                    break;
                case DateRanges.Year:
                    contributorStatsDateFrom = DateTimeConsts.LastYear;
                    break;
                case DateRanges.All:
                default:
                    break;
            }

            var contributorStatsDateFromString = (contributorStatsDateFrom).ToString(DateTimeConsts.DateTimeFormat);
            ContributorStatsDateRange = $"{contributorStatsDateFromString} - {contributorStatsDateToString}";
            var contributorStats = await _contributorService.GetContributorStats(contributorId, contributorStatsDateFrom, DateTime.UtcNow);
            ContributorStatsDto contributorStatsIncrement;

            if (dateRange == DateRanges.All)
            {
                var previousContributorStats = await _contributorService.GetContributorStats(contributorId, DateTimeConsts.UnixEpoch, DateTimeConsts.Yesterday);

                contributorStatsIncrement = new ContributorStatsDto
                {
                    Commits = contributorStats.Commits - previousContributorStats.Commits,
                    Merges = contributorStats.Merges - previousContributorStats.Merges,
                    LinesOfCode = contributorStats.LinesOfCode - previousContributorStats.LinesOfCode,
                    ContribToProject = contributorStats.ContribToProject - previousContributorStats.ContribToProject
                };
            }
            else
            {
                contributorStatsIncrement = new ContributorStatsDto
                {
                    Commits = int.MinValue,
                    Merges = int.MinValue,
                    LinesOfCode = int.MinValue,
                    ContribToProject = double.MinValue
                };
            }

            ContributorStats = contributorStats;
            ContributorStatsIncrement = contributorStatsIncrement;

            IsLoading = false;
        }
    }
}
