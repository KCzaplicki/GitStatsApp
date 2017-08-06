using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GitStatsApp.Consts;
using GitStatsApp.Dtos;
using GitStatsApp.Enums;
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
        public RepositoryDto Repository {
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

        public RepositoryViewModel(IRepositoryService repositoryService, INavigationService navigationService)
        {
            _repositoryService = repositoryService;
            _navigationService = navigationService;
            ContributorSelectedCommand = new RelayCommand<ContributorDto>((contributor) => NavigateToContributorPage(contributor));
        }

        public async Task LoadRepositoryContributors(string repositoryId)
        {
            var serviceContributors = await _repositoryService.GetRepositoryContributors(repositoryId);
            Contributors = new ObservableCollection<ContributorDto>(serviceContributors);

            RaisePropertyChanged(() => Contributors);
        }

        private void NavigateToContributorPage(ContributorDto contributor)
        {
            _navigationService.NavigateTo(Pages.ContributorPage, new Tuple<RepositoryDto, ContributorDto>(repository, contributor));
        }
    }
}
