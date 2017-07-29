using Xamarin.Forms;

namespace GitStatsApp.Tests
{
    public class NavigationServiceTestsFixture
    {
        public StubMainPage MainPage { get; }
        public StubDetailsPage DetailsPage { get; }
        public StubParameterPage ParameterPage { get; }
        public string ParameterPageParameter { get; }

        public NavigationServiceTestsFixture()
        {
            MainPage = new StubMainPage();
            DetailsPage = new StubDetailsPage();
            ParameterPageParameter = "Parameter";
            ParameterPage = new StubParameterPage(ParameterPageParameter);
        }
    }

    public class StubMainPage : ContentPage
    {
    }

    public class StubDetailsPage : ContentPage
    {
    }

    public class StubParameterPage : ContentPage
    {
        public StubParameterPage(string Parameter)
        {
        }
    }
}
