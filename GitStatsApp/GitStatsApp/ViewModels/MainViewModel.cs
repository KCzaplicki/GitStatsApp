using GalaSoft.MvvmLight;
using GitStatsApp.Dtos;
using GitStatsApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GitStatsApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string Title { get; } = "GitStatsApp";

        public ObservableCollection<RepositoryDto> Repositories { get; set; }

        private IRepositoryService _repositoryService;

        public MainViewModel(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;

            Task.Run(() => Initialize());
        }

        private async Task Initialize()
        {
            var serviceRepositories = await _repositoryService.GetRepositories();
            Repositories = new ObservableCollection<RepositoryDto>(serviceRepositories);
            RaisePropertyChanged(() => Repositories);
        }
    }
}