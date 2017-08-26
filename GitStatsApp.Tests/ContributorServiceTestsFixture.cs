using GitStatsApp.Dtos;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System;
using GitStatsApp.Helpers;
using System.Net;
using GitStatsApp.Consts;

namespace GitStatsApp.Tests
{
    public class ContributorServiceTestsFixture
    {
        public MockHttpMessageHandler HttpMessageHandler { get; }
        public MockHttpMessageHandler EmptyResultsHttpMessageHandler { get;}
        public MockHttpMessageHandler ErrorStatusCodeHttpMessageHandler { get; set; }
        public ContributorStatsDto UnlimitedDateContributor { get; }
        public ContributorStatsDto LimitedDateContributor { get; }
        public string UnlimitedDateContributorId { get; }
        public string UnlimitedDateContributorRepositoryId { get; }
        public string UnlimitedDateContributorName { get; }
        public string UnlimitedDateContributorEmail { get; }
        public int UnlimitedDateContributorCommits { get; }
        public int UnlimitedDateContributorMerges { get; }
        public int UnlimitedDateContributorLinesOfCode { get; }
        public double UnlimitedDateContributorContribToProject { get; }
        public DateTime LimitedDateContributorFrom { get; set; }
        public DateTime LimitedDateContributorTo { get; set; }
        public string LimitedDateContributorId { get; }
        public string LimitedDateContributorRepositoryId { get; }
        public string LimitedDateContributorName { get; }
        public string LimitedDateContributorEmail { get; }
        public int LimitedDateContributorCommits { get; }
        public int LimitedDateContributorMerges { get; }
        public int LimitedDateContributorLinesOfCode { get; }
        public double LimitedDateContributorContribToProject { get; }

        public ContributorServiceTestsFixture()
        {
            UnlimitedDateContributorId = "U1d4c2RYTjBjbUYwYVc5dUxYZGxZZz09OktyeXN0aWFuIEN6YXBsaWNraTprcnlzdGlhbi5jemFwbGlja2lAaW50aXZlLmNvbQ==";
            UnlimitedDateContributorRepositoryId = "SWxsdXN0cmF0aW9uLXdlYg==";
            UnlimitedDateContributorName = "Krystian Czaplicki";
            UnlimitedDateContributorEmail = "krystian.czaplicki@outlook.com";
            UnlimitedDateContributorCommits = 5;
            UnlimitedDateContributorMerges = 1;
            UnlimitedDateContributorLinesOfCode = 103;
            UnlimitedDateContributorContribToProject = 0.321;

            LimitedDateContributorId = "U1d4c2RYTjBjbUYwYVc5dUxYZGxZZz09OktyeXN0aWFuIEN6YXBsaWNraTprcnlzdGlhbi5jemFwbGlja2lAaW50aXZlLmNvbQ==";
            LimitedDateContributorRepositoryId = "SWxsdXN0cmF0aW9uLXdlYg==";
            LimitedDateContributorName = "Krystian Czaplicki";
            LimitedDateContributorEmail = "krystian.czaplicki@outlook.com";
            LimitedDateContributorCommits = 15;
            LimitedDateContributorMerges = 3;
            LimitedDateContributorLinesOfCode = 303;
            LimitedDateContributorContribToProject = 0.432;

            UnlimitedDateContributor = new ContributorStatsDto
            {
                Id = UnlimitedDateContributorId,
                RepositoryId = UnlimitedDateContributorRepositoryId,
                Name = UnlimitedDateContributorName,
                Email = UnlimitedDateContributorEmail,
                Commits = UnlimitedDateContributorCommits,
                Merges = UnlimitedDateContributorMerges,
                LinesOfCode = UnlimitedDateContributorLinesOfCode,
                ContribToProject = UnlimitedDateContributorContribToProject
            };

            LimitedDateContributor = new ContributorStatsDto
            {
                Id = LimitedDateContributorId,
                RepositoryId = LimitedDateContributorRepositoryId,
                Name = LimitedDateContributorName,
                Email = LimitedDateContributorEmail,
                Commits = LimitedDateContributorCommits,
                Merges = LimitedDateContributorMerges,
                LinesOfCode = LimitedDateContributorLinesOfCode,
                ContribToProject = LimitedDateContributorContribToProject
            };

            LimitedDateContributorFrom = new DateTime(2010, 1, 1, 8, 0, 0, DateTimeKind.Utc);
            LimitedDateContributorTo = new DateTime(2015, 1, 1, 8, 0, 0, DateTimeKind.Utc);

            HttpMessageHandler = new MockHttpMessageHandler();
            HttpMessageHandler.When(string.Format(RestApiUrlsConsts.GetContributorStatsUrl, UnlimitedDateContributorId))
                .Respond("application/json", JsonConvert.SerializeObject(UnlimitedDateContributor));
            HttpMessageHandler.When(string.Format(RestApiUrlsConsts.GetContributorStatsWithRangeUrl,
                LimitedDateContributorId, 
                LimitedDateContributorFrom.ToUnixTimestamp(), 
                LimitedDateContributorTo.ToUnixTimestamp()))
                .Respond("application/json", JsonConvert.SerializeObject(LimitedDateContributor));
            HttpMessageHandler.When(string.Format(RestApiUrlsConsts.GetContributorStatsWithRangeUrl,
                LimitedDateContributorId,
                LimitedDateContributorFrom.ToUnixTimestamp(),
                "*"))
                .Respond("application/json", JsonConvert.SerializeObject(LimitedDateContributor));

            EmptyResultsHttpMessageHandler = new MockHttpMessageHandler();
            EmptyResultsHttpMessageHandler.When(string.Format(RestApiUrlsConsts.GetContributorStatsUrl, UnlimitedDateContributorId))
                .Respond("application/json", "[]");
            EmptyResultsHttpMessageHandler.When(string.Format(RestApiUrlsConsts.GetContributorStatsWithRangeUrl,
                LimitedDateContributorId,
                LimitedDateContributorFrom.ToUnixTimestamp(),
                LimitedDateContributorTo.ToUnixTimestamp()))
                .Respond("application/json", "[]");
            EmptyResultsHttpMessageHandler.When(string.Format(RestApiUrlsConsts.GetContributorStatsWithRangeUrl,
                LimitedDateContributorId,
                LimitedDateContributorFrom.ToUnixTimestamp(),
                "*"))
                .Respond("application/json", "[]");

            ErrorStatusCodeHttpMessageHandler = new MockHttpMessageHandler();
            ErrorStatusCodeHttpMessageHandler.When(string.Format(RestApiUrlsConsts.GetContributorStatsUrl, UnlimitedDateContributorId))
                .Respond(HttpStatusCode.BadRequest);
            ErrorStatusCodeHttpMessageHandler.When(string.Format(RestApiUrlsConsts.GetContributorStatsWithRangeUrl,
                LimitedDateContributorId,
                LimitedDateContributorFrom.ToUnixTimestamp(),
                LimitedDateContributorTo.ToUnixTimestamp()))
                .Respond(HttpStatusCode.BadRequest);
            ErrorStatusCodeHttpMessageHandler.When(string.Format(RestApiUrlsConsts.GetContributorStatsWithRangeUrl,
                LimitedDateContributorId,
                LimitedDateContributorFrom.ToUnixTimestamp(),
                LimitedDateContributorTo.ToUnixTimestamp()))
                .Respond(HttpStatusCode.BadRequest);
        }
    }
}
