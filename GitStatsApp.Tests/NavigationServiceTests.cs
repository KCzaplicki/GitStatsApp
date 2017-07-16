using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitStatsApp.Services;
using Xamarin.Forms;

namespace GitStatsApp.Tests
{
    [TestClass]
    public class NavigationServiceTests
    {
        private NavigationService _navigationService;
        private NavigationServiceTestsFixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _navigationService = new NavigationService();
            _fixture = new NavigationServiceTestsFixture();
        }

        [TestMethod]
        public void Should_StartWithPageFromConstrutor_When_PageSetInConstructor()
        {
            // Arrange
            var navigation = new NavigationPage(_fixture.MainPage);
            _navigationService.Configure(_fixture.MainPage.GetType());
            _navigationService.Initialize(navigation);

            // Act
            var currentPageKey = _navigationService.CurrentPageKey;

            // Assert
            Assert.AreEqual(_fixture.MainPage.GetType().Name, currentPageKey);
        }

        [TestMethod]
        public void Should_StartWithoutPageFromConstrutor_When_PageNotSetInConfiguration()
        {
            // Arrange
            var navigation = new NavigationPage(_fixture.MainPage);
            _navigationService.Initialize(navigation);

            // Act
            var currentPageKey = _navigationService.CurrentPageKey;

            // Assert
            Assert.IsNull(currentPageKey);
        }

        [TestMethod]
        public void Should_NavigateToPage_When_PageIsProperlyConfigured()
        {
            // Arrange
            var navigation = new NavigationPage(_fixture.MainPage);
            _navigationService.Configure(_fixture.MainPage.GetType());
            _navigationService.Configure(_fixture.DetailsPage.GetType());
            _navigationService.Initialize(navigation);

            // Act
            var currentPageKeyOld = _navigationService.CurrentPageKey;
            _navigationService.NavigateTo(_fixture.DetailsPage.GetType().Name);
            var currentPageKeyNew = _navigationService.CurrentPageKey;

            // Assert
            Assert.AreEqual(_fixture.MainPage.GetType().Name,currentPageKeyOld);
            Assert.AreNotEqual(currentPageKeyOld, currentPageKeyNew);
            Assert.AreEqual(_fixture.DetailsPage.GetType().Name, currentPageKeyNew);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_ThrowExceptionOnNavigateTo_When_PageIsNotConfigured()
        {
            // Arrange
            var navigation = new NavigationPage(_fixture.MainPage);
            _navigationService.Configure(_fixture.MainPage.GetType());
            _navigationService.Initialize(navigation);

            // Act
            var currentPageKeyOld = _navigationService.CurrentPageKey;
            _navigationService.NavigateTo(_fixture.DetailsPage.GetType().Name);
            var currentPageKeyNew = _navigationService.CurrentPageKey;

            // Assert
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Should_ThrowExceptionOnNavigateTo_When_NavigateToPageWithParameterAndDoNotSetParameter()
        {
            // Arrange
            var navigation = new NavigationPage(_fixture.MainPage);
            _navigationService.Configure(_fixture.MainPage.GetType());
            _navigationService.Configure(_fixture.ParameterPage.GetType());
            _navigationService.Initialize(navigation);

            // Act
            var currentPageKeyOld = _navigationService.CurrentPageKey;
            _navigationService.NavigateTo(_fixture.ParameterPage.GetType().Name);
            var currentPageKeyNew = _navigationService.CurrentPageKey;

            // Assert
        }

        [TestMethod]
        public void Should_NavigateToPage_When_NavigateToPageWithParameterAndParameterIsSet()
        {
            // Arrange
            var navigation = new NavigationPage(_fixture.MainPage);
            _navigationService.Configure(_fixture.MainPage.GetType());
            _navigationService.Configure(_fixture.ParameterPage.GetType());
            _navigationService.Initialize(navigation);

            // Act
            var currentPageKeyOld = _navigationService.CurrentPageKey;
            _navigationService.NavigateTo(_fixture.ParameterPage.GetType().Name, _fixture.ParameterPageParameter);
            var currentPageKeyNew = _navigationService.CurrentPageKey;

            // Assert
            Assert.AreEqual(_fixture.MainPage.GetType().Name, currentPageKeyOld);
            Assert.AreNotEqual(currentPageKeyOld, currentPageKeyNew);
            Assert.AreEqual(_fixture.ParameterPage.GetType().Name, currentPageKeyNew);
        }

        [TestMethod]
        public void Should_GoBack_When_WentToAnotherPageEarlier()
        {
            // Arrange
            var navigation = new NavigationPage(_fixture.MainPage);
            _navigationService.Configure(_fixture.MainPage.GetType());
            _navigationService.Configure(_fixture.DetailsPage.GetType());
            _navigationService.Initialize(navigation);
            _navigationService.NavigateTo(_fixture.DetailsPage.GetType().Name);

            // Act
            var currentPageKeyOld = _navigationService.CurrentPageKey;
            _navigationService.GoBack();
            var currentPageKeyNew = _navigationService.CurrentPageKey;

            // Assert
            Assert.AreEqual(_fixture.DetailsPage.GetType().Name, currentPageKeyOld);
            Assert.AreNotEqual(currentPageKeyOld, currentPageKeyNew);
            Assert.AreEqual(_fixture.MainPage.GetType().Name, currentPageKeyNew);
        }

        [TestMethod]
        public void Should_StayOnTheSamePage_When_DidNotGoToAnotherPageEarlier()
        {
            // Arrange
            var navigation = new NavigationPage(_fixture.MainPage);
            _navigationService.Configure(_fixture.MainPage.GetType());
            _navigationService.Initialize(navigation);

            // Act
            var currentPageKeyOld = _navigationService.CurrentPageKey;
            _navigationService.GoBack();
            var currentPageKeyNew = _navigationService.CurrentPageKey;

            // Assert
            Assert.AreEqual(_fixture.MainPage.GetType().Name, currentPageKeyOld);
            Assert.AreEqual(currentPageKeyOld, currentPageKeyNew);
        }
    }
}
