using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GitStatsApp.Services;
using GitStatsApp.ViewModels;
using GitStatsApp.Views;
using System.Net.Http;
using Xamarin.Forms;

namespace GitStatsApp
{
    public partial class App : Application
    {
        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator { get { return _locator ?? (_locator = new ViewModelLocator()); } }

        public App()
        {
            var nav = new NavigationService();
            nav.Configure(typeof(MainPage));

            SimpleIoc.Default.Register<INavigationService>(() => nav);
            SimpleIoc.Default.Register<HttpClient>(() => new HttpClient());
            SimpleIoc.Default.Register<IRepositoryService, RepositoryService>();
            SimpleIoc.Default.Register<IContributorService, ContributorService>();

            var mainPage = new NavigationPage(new MainPage());

            nav.Initialize(mainPage);

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
