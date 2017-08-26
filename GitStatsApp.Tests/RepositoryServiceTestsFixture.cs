using GitStatsApp.Consts;
using GitStatsApp.Dtos;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System.Collections.Generic;
using System.Net;

namespace GitStatsApp.Tests
{
    public class RepositoryServiceTestsFixture
    {
        public MockHttpMessageHandler HttpMessageHandler { get; }
        public MockHttpMessageHandler ErrorStatusCodeHttpMessageHandler { get; }
        public MockHttpMessageHandler EmptyResultsHttpMessageHandler { get; }
        public List<RepositoryDto> Repositories { get; }
        public List<ContributorDto> Contributors { get; }
        public string FirstRepositoryId { get; }
        public string FirstRepositoryName { get; }
        public string FirstRepositoryUrl { get; }
        public string SecondRepositoryId { get; }
        public string SecondRepositoryName { get; }
        public string SecondRepositoryUrl { get; }
        public int RepositoriesCount { get; }
        public string FirstContributorId { get; }
        public string FirstContributorName { get; }
        public string FirstContributorEmail { get; }
        public int FirstContributorCommits { get; }
        public string SecondContributorId { get; }
        public string SecondContributorName { get; }
        public string SecondContributorEmail { get; }
        public int SecondContributorCommits { get; }
        public int ContributorsCount { get; }
        public string NonExistingRepositoryId { get; }

        public RepositoryServiceTestsFixture()
        {
            FirstRepositoryId = "SWxsdXN0cmF0aW9uLXdlYg==";
            FirstRepositoryName = "Mock First Repository";
            FirstRepositoryUrl = "bitbucket.org/stash/projects/csharp/repos/core";

            SecondRepositoryId = "SWxsdXN0cmF0aW9uLW1vYmlsZQ==";
            SecondRepositoryName = "Mock Second Repository";
            SecondRepositoryUrl = "bitbucket.org/stash/projects/aspnet/repos/core";

            Repositories = new List<RepositoryDto>
            {
                new RepositoryDto
                {
                    Id = FirstRepositoryId,
                    Name = FirstRepositoryName,
                    Url = FirstRepositoryUrl
                },
                new RepositoryDto
                {
                    Id = SecondRepositoryId,
                    Name = SecondRepositoryName,
                    Url = SecondRepositoryUrl
                }
            };
            RepositoriesCount = Repositories.Count;

            FirstContributorId = "U1d4c2RYTjBjbUYwYVc5dUxYZGxZZz09OktyeXN0aWFuIEN6YXBsaWNraTprcnlzdGlhbi5jemFwbGlja2lAaW50aXZlLmNvbQ==";
            FirstContributorName = "Krystian Czaplicki";
            FirstContributorEmail = "krystian.czaplicki@outlook.com";
            FirstContributorCommits = 5;

            SecondContributorId = "U1d4c2RYTjBjbUYwYVc5dUxYZGxZZz09OlBhd2VsIEJvcm93c2tpOnBhd2VsLmJvcm93c2tpQGludGl2ZS5jb20=";
            SecondContributorName = "John Page";
            SecondContributorEmail = "john.page@outlook.com";
            SecondContributorCommits = 15;

            Contributors = new List<ContributorDto>
            {
                new ContributorDto
                {
                    Id = FirstContributorId,
                    Name = FirstContributorName,
                    Email = FirstContributorEmail,
                    Commits = FirstContributorCommits
                },
                new ContributorDto
                {
                    Id = SecondContributorId,
                    Name = SecondContributorName,
                    Email = SecondContributorEmail,
                    Commits = SecondContributorCommits
                }
            };
            ContributorsCount = Contributors.Count;

            NonExistingRepositoryId = "SWxsdXN0cmF0aW9uLWVuZ2luZQ==";

            HttpMessageHandler = new MockHttpMessageHandler();
            HttpMessageHandler.When(RestApiUrlsConsts.GetRepositoriesUrl)
                .Respond("application/json", JsonConvert.SerializeObject(Repositories));
            HttpMessageHandler.When(string.Format(RestApiUrlsConsts.GetRepositoryContributorsUrl, FirstRepositoryId))
                .Respond("application/json", JsonConvert.SerializeObject(Contributors));

            ErrorStatusCodeHttpMessageHandler = new MockHttpMessageHandler();
            ErrorStatusCodeHttpMessageHandler.When(RestApiUrlsConsts.GetRepositoriesUrl)
                .Respond(HttpStatusCode.BadRequest);
            HttpMessageHandler.When(string.Format(RestApiUrlsConsts.GetRepositoryContributorsUrl, FirstRepositoryId))
                .Respond(HttpStatusCode.BadRequest);

            EmptyResultsHttpMessageHandler = new MockHttpMessageHandler();
            EmptyResultsHttpMessageHandler.When(RestApiUrlsConsts.GetRepositoriesUrl)
                .Respond("application/json", "{}");
            EmptyResultsHttpMessageHandler.When(string.Format(RestApiUrlsConsts.GetRepositoryContributorsUrl, FirstRepositoryId))
                .Respond("application/json", "{}");
        }
    }
}
