using GitStatsApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitStatsApp.Tests
{
    [TestClass]
    public class RepositoryServiceTests
    {
        private IRepositoryService _repositoryService;
        private RepositoryServiceTestsFixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new RepositoryServiceTestsFixture();
            var httpClient = new HttpClient(_fixture.HttpMessageHandler);
            _repositoryService = new RepositoryService(httpClient);
        }

        [TestMethod]
        public async Task Should_GetRepositories()
        {
            // Arrange

            // Act
            var repositories = await _repositoryService.GetRepositories();

            // Assert
            Assert.AreEqual(_fixture.RepositoriesCount, repositories.Count);

            var firstRepository = repositories[0];
            Assert.AreEqual(_fixture.FirstRepositoryId, firstRepository.Id);
            Assert.AreEqual(_fixture.FirstRepositoryName, firstRepository.Name);
            Assert.AreEqual(_fixture.FirstRepositoryUrl, firstRepository.Url);

            var secondRepository = repositories[1];
            Assert.AreEqual(_fixture.SecondRepositoryId, secondRepository.Id);
            Assert.AreEqual(_fixture.SecondRepositoryName, secondRepository.Name);
            Assert.AreEqual(_fixture.SecondRepositoryUrl, secondRepository.Url);
        }

        [TestMethod]
        public async Task Should_ReturnNull_When_ApiRepositoryReturnErrorStatusCode()
        {
            // Arrange
            var httpClient = new HttpClient(_fixture.ErrorStatusCodeHttpMessageHandler);
            _repositoryService = new RepositoryService(httpClient);

            // Act
            var repositories = await _repositoryService.GetRepositories();

            // Assert
            Assert.IsNull(repositories);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public async Task Should_ThrowException_When_ApiRepositoryReturnInvalidResponse()
        {
            // Arrange
            var httpClient = new HttpClient(_fixture.EmptyResultsHttpMessageHandler);
            _repositoryService = new RepositoryService(httpClient);

            // Act
            var repositories = await _repositoryService.GetRepositories();
        }

        [TestMethod]
        public async Task Should_GetRepositoryContributors()
        {
            // Arrange
            var repositoryId = _fixture.FirstRepositoryId;

            // Act
            var contributors = await _repositoryService.GetRepositoryContributors(repositoryId);

            // Assert
            Assert.AreEqual(_fixture.ContributorsCount, contributors.Count);

            var firstContributor = contributors[0];
            Assert.AreEqual(_fixture.FirstContributorId, firstContributor.Id);
            Assert.AreEqual(_fixture.FirstContributorName, firstContributor.Name);
            Assert.AreEqual(_fixture.FirstContributorEmail, firstContributor.Email);
            Assert.AreEqual(_fixture.FirstContributorCommits, firstContributor.Commits);
            
            var secondContributor = contributors[1];
            Assert.AreEqual(_fixture.SecondContributorId, secondContributor.Id);
            Assert.AreEqual(_fixture.SecondContributorName, secondContributor.Name);
            Assert.AreEqual(_fixture.SecondContributorEmail, secondContributor.Email);
            Assert.AreEqual(_fixture.SecondContributorCommits, secondContributor.Commits);
        }

        [TestMethod]
        public async Task Should_ReturnNull_When_ApiRepositoryContributorsReturnErrorStatusCode()
        {
            // Arrange
            var repositoryId = _fixture.FirstRepositoryId;
            var httpClient = new HttpClient(_fixture.ErrorStatusCodeHttpMessageHandler);
            _repositoryService = new RepositoryService(httpClient);

            // Act
            var repositories = await _repositoryService.GetRepositoryContributors(repositoryId);

            // Assert
            Assert.IsNull(repositories);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public async Task Should_ThrowException_When_ApiRepositoryContributorsReturnInvalidResponse()
        {
            // Arrange
            var repositoryId = _fixture.FirstRepositoryId;
            var httpClient = new HttpClient(_fixture.EmptyResultsHttpMessageHandler);
            _repositoryService = new RepositoryService(httpClient);

            // Act
            var repositories = await _repositoryService.GetRepositoryContributors(repositoryId);
        }

        [TestMethod]
        public async Task Should_ReturnNull_When_RepositoryNotFound()
        {
            // Arrange
            var repositoryId = _fixture.FirstRepositoryId;
            var httpClient = new HttpClient(_fixture.ErrorStatusCodeHttpMessageHandler);
            _repositoryService = new RepositoryService(httpClient);

            // Act
            var repositories = await _repositoryService.GetRepositoryContributors(repositoryId);

            // Assert
            Assert.IsNull(repositories);
        }
    }
}
