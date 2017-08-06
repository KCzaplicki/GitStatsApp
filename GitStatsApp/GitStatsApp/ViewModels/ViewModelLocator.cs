using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace GitStatsApp.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<RepositoryViewModel>();
            SimpleIoc.Default.Register<ContributorViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public RepositoryViewModel Repository
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RepositoryViewModel>();
            }
        }

        public ContributorViewModel Contributor
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ContributorViewModel>();
            }
        }

        public static void Cleanup()
        {
        }
    }
}