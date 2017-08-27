using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GitStatsApp.Consts;
using GitStatsApp.Services;
using GitStatsApp.ViewModels;
using GitStatsApp.Views;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
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
            MobileCenter.Start($"android={AppConsts.AndroidSecretKey};ios={AppConsts.IosSecrectKey}", typeof(Analytics), typeof(Crashes));

            var nav = new NavigationService();
            nav.Configure(typeof(MainPage));
            nav.Configure(typeof(RepositoryPage));
            nav.Configure(typeof(ContributorPage));

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
