using GitStatsApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitStatsApp.Tests
{
    [TestClass]
    public class ContributorServiceTests
    {
        private IContributorService _contributorService;
        private ContributorServiceTestsFixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new ContributorServiceTestsFixture();
            var httpClient = new HttpClient(_fixture.HttpMessageHandler);
            _contributorService = new ContributorService(httpClient);
        }

        [TestMethod]
        public async Task Should_GetContributorStats()
        {
            // Arrange
            var contributorId = _fixture.UnlimitedDateContributorId;

            // Act
            var contributor = await _contributorService.GetContributorStats(contributorId);

            // Assert
            Assert.IsNotNull(contributor);
            
            Assert.AreEqual(_fixture.UnlimitedDateContributorId, contributor.Id);
            Assert.AreEqual(_fixture.UnlimitedDateContributorRepositoryId, contributor.RepositoryId);
            Assert.AreEqual(_fixture.UnlimitedDateContributorName, contributor.Name);
            Assert.AreEqual(_fixture.UnlimitedDateContributorEmail, contributor.Email);
            Assert.AreEqual(_fixture.UnlimitedDateContributorCommits, contributor.Commits);
            Assert.AreEqual(_fixture.UnlimitedDateContributorMerges, contributor.Merges);
            Assert.AreEqual(_fixture.UnlimitedDateContributorLinesOfCode, contributor.LinesOfCode);
            Assert.AreEqual(_fixture.UnlimitedDateContributorContribToProject, contributor.ContribToProject);
        }

        [TestMethod]
        public async Task Should_GetContributorStatsInDateRange()
        {
            // Arrange
            var contributorId = _fixture.UnlimitedDateContributorId;
            var from = _fixture.LimitedDateContributorFrom;
            var to = _fixture.LimitedDateContributorTo;

            // Act
            var contributor = await _contributorService.GetContributorStats(contributorId, from, to);

            // Assert
            Assert.IsNotNull(contributor);

            Assert.AreEqual(_fixture.LimitedDateContributorId, contributor.Id);
            Assert.AreEqual(_fixture.LimitedDateContributorRepositoryId, contributor.RepositoryId);
            Assert.AreEqual(_fixture.LimitedDateContributorName, contributor.Name);
            Assert.AreEqual(_fixture.LimitedDateContributorEmail, contributor.Email);
            Assert.AreEqual(_fixture.LimitedDateContributorCommits, contributor.Commits);
            Assert.AreEqual(_fixture.LimitedDateContributorMerges, contributor.Merges);
            Assert.AreEqual(_fixture.LimitedDateContributorLinesOfCode, contributor.LinesOfCode);
            Assert.AreEqual(_fixture.LimitedDateContributorContribToProject, contributor.ContribToProject);
        }

        [TestMethod]
        public async Task Should_GetContributorStatsFromDate()
        {
            // Arrange
            var contributorId = _fixture.UnlimitedDateContributorId;
            var from = _fixture.LimitedDateContributorFrom;

            // Act
            var contributor = await _contributorService.GetContributorStats(contributorId, from);

            // Assert
            Assert.IsNotNull(contributor);

            Assert.AreEqual(_fixture.LimitedDateContributorId, contributor.Id);
            Assert.AreEqual(_fixture.LimitedDateContributorRepositoryId, contributor.RepositoryId);
            Assert.AreEqual(_fixture.LimitedDateContributorName, contributor.Name);
            Assert.AreEqual(_fixture.LimitedDateContributorEmail, contributor.Email);
            Assert.AreEqual(_fixture.LimitedDateContributorCommits, contributor.Commits);
            Assert.AreEqual(_fixture.LimitedDateContributorMerges, contributor.Merges);
            Assert.AreEqual(_fixture.LimitedDateContributorLinesOfCode, contributor.LinesOfCode);
            Assert.AreEqual(_fixture.LimitedDateContributorContribToProject, contributor.ContribToProject);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public async Task Should_ThrowException_When_ApiContributorReturnInvalidResponse()
        {
            // Arrange
            var contributorId = _fixture.UnlimitedDateContributorId;
            var httpClient = new HttpClient(_fixture.EmptyResultsHttpMessageHandler);
            _contributorService = new ContributorService(httpClient);

            // Act
            var contributor = await _contributorService.GetContributorStats(contributorId);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public async Task Should_ThrowException_When_ApiContributorInDateRangeReturnInvalidResponse()
        {
            // Arrange
            var contributorId = _fixture.LimitedDateContributorId;
            var from = _fixture.LimitedDateContributorFrom;
            var to = _fixture.LimitedDateContributorTo;
            var httpClient = new HttpClient(_fixture.EmptyResultsHttpMessageHandler);
            _contributorService = new ContributorService(httpClient);

            // Act
            var contributor = await _contributorService.GetContributorStats(contributorId, from, to);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public async Task Should_ThrowException_When_ApiContributorFromRangeReturnInvalidResponse()
        {
            // Arrange
            var contributorId = _fixture.LimitedDateContributorId;
            var from = _fixture.LimitedDateContributorFrom;
            var httpClient = new HttpClient(_fixture.EmptyResultsHttpMessageHandler);
            _contributorService = new ContributorService(httpClient);

            // Act
            var contributor = await _contributorService.GetContributorStats(contributorId, from);
        }

        [TestMethod]
        public async Task Should_ReturnNull_When_ApiContributorReturnErrorStatusCode()
        {
            // Arrange
            var contributorId = _fixture.UnlimitedDateContributorId;
            var httpClient = new HttpClient(_fixture.ErrorStatusCodeHttpMessageHandler);
            _contributorService = new ContributorService(httpClient);

            // Act
            var repositories = await _contributorService.GetContributorStats(contributorId);

            // Assert
            Assert.IsNull(repositories);
        }

        [TestMethod]
        public async Task Should_ReturnNull_When_ApiContributorInDateRangeReturnErrorStatusCode()
        {
            // Arrange
            var contributorId = _fixture.LimitedDateContributorId;
            var from = _fixture.LimitedDateContributorFrom;
            var to = _fixture.LimitedDateContributorTo;
            var httpClient = new HttpClient(_fixture.ErrorStatusCodeHttpMessageHandler);
            _contributorService = new ContributorService(httpClient);

            // Act
            var repositories = await _contributorService.GetContributorStats(contributorId, from, to);

            // Assert
            Assert.IsNull(repositories);
        }

        [TestMethod]
        public async Task Should_ReturnNull_When_ApiContributorFromRangeReturnErrorStatusCode()
        {
            // Arrange
            var contributorId = _fixture.LimitedDateContributorId;
            var from = _fixture.LimitedDateContributorFrom;
            var httpClient = new HttpClient(_fixture.ErrorStatusCodeHttpMessageHandler);
            _contributorService = new ContributorService(httpClient);

            // Act
            var repositories = await _contributorService.GetContributorStats(contributorId, from);

            // Assert
            Assert.IsNull(repositories);
        }
    }
}
