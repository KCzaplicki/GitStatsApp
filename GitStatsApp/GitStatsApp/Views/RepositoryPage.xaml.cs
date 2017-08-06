using GitStatsApp.Dtos;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GitStatsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RepositoryPage : ContentPage
    {
        public RepositoryPage(RepositoryDto repository)
        {
            InitializeComponent();
            var viewModel = App.Locator.Repository;
            BindingContext = viewModel;
            viewModel.Repository = repository;

            Task.Run(() => viewModel.LoadRepositoryContributors(repository.Id));
        }
    }
}
