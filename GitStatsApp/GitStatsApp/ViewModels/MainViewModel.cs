using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GitStatsApp.Dtos;
using GitStatsApp.Enums;
using GitStatsApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GitStatsApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string Title { get; } = "GitStatsApp";

        public ObservableCollection<RepositoryDto> Repositories { get; set; }

        public RelayCommand<RepositoryDto> RepositorySelectedCommand { get; private set; }

        private IRepositoryService _repositoryService;
        private INavigationService _navigationService;

        public MainViewModel(IRepositoryService repositoryService, INavigationService navigationService)
        {
            _repositoryService = repositoryService;
            _navigationService = navigationService;
            RepositorySelectedCommand = new RelayCommand<RepositoryDto>((repository) => NavigateToRepositoryPage(repository));

            Task.Run(() => Initialize());
        }

        private async Task Initialize()
        {
            var serviceRepositories = await _repositoryService.GetRepositories();
            Repositories = new ObservableCollection<RepositoryDto>(serviceRepositories);
            RaisePropertyChanged(() => Repositories);
        }

        private void NavigateToRepositoryPage(RepositoryDto repository)
        {
            _navigationService.NavigateTo(Pages.RepositoryPage, repository);
        }
    }
}