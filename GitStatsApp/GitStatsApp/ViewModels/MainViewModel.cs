using GalaSoft.MvvmLight;

namespace GitStatsApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string Title
        {
            get
            {
                return "GitStatsApp";
            }
        }

        public MainViewModel()
        {
        }
    }
}