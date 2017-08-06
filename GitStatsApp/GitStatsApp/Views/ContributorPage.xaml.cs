using GitStatsApp.Dtos;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GitStatsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContributorPage : ContentPage
    {
        public ContributorPage(Tuple<RepositoryDto, ContributorDto> repositoryContributorTuple)
        {
            (var repository, var contributor) = repositoryContributorTuple;

            InitializeComponent();
            var viewModel = App.Locator.Contributor;
            BindingContext = viewModel;
            viewModel.Repository = repository;
            viewModel.Contributor = contributor;

            Task.Run(() => viewModel.LoadContributorStats(contributor.Id));
        }
    }
}
