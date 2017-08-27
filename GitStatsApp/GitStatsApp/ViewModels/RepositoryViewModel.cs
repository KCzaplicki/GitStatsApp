using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GitStatsApp.Consts;
using GitStatsApp.Dtos;
using GitStatsApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GitStatsApp.ViewModels
{
    public class RepositoryViewModel : ViewModelBase
    {
        public string Title { get; } = AppConsts.AppName;
        public string ContributorsHeader { get; } = "Contributors";
        public string ContributorsIcon { get; } = "users.png";

        public ObservableCollection<ContributorDto> Contributors { get; set; }
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
        public RelayCommand<ContributorDto> ContributorSelectedCommand { get; private set; }

        private RepositoryDto repository;
        private IRepositoryService _repositoryService;
        private INavigationService _navigationService;
        private bool isLoading;

        public RepositoryViewModel(IRepositoryService repositoryService, INavigationService navigationService)
        {
            _repositoryService = repositoryService;
            _navigationService = navigationService;
            ContributorSelectedCommand = new RelayCommand<ContributorDto>((contributor) => NavigateToContributorPage(contributor));
        }

        public async Task LoadRepositoryContributors(string repositoryId)
        {
            IsLoading = true;

            var serviceContributors = await _repositoryService.GetRepositoryContributors(repositoryId);
            Contributors = new ObservableCollection<ContributorDto>(serviceContributors);

            RaisePropertyChanged(() => Contributors);

            IsLoading = false;
        }

        private void NavigateToContributorPage(ContributorDto contributor)
        {
            _navigationService.NavigateTo(PagesConsts.ContributorPage, new Tuple<RepositoryDto, ContributorDto>(repository, contributor));
        }
    }
}
