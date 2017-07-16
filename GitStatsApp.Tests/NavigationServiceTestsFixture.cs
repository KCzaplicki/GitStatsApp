using Xamarin.Forms;

namespace GitStatsApp.Tests
{
    public class NavigationServiceTestsFixture : ContentPage
    {
        public StubMainPage MainPage { get; set; }
        public StubDetailsPage DetailsPage { get; set; }
        public StubParameterPage ParameterPage { get; set; }
        public string ParameterPageParameter { get; set; }

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
