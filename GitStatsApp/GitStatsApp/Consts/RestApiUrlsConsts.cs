namespace GitStatsApp.Consts
{
    public class RestApiUrlsConsts
    {
        public static readonly string ApiUrl = "http://gitstatsapp.cloudapp.net/api";

        public static readonly string GetRepositoriesUrl = $"{ApiUrl}/repository";
        public static readonly string GetRepositoryContributorsUrl = $"{ApiUrl}/repository/{{0}}";
        public static readonly string GetContributorStatsUrl = $"{ApiUrl}/contributor/{{0}}";
        public static readonly string GetContributorStatsWithRangeUrl = $"{ApiUrl}/contributor/{{0}}/{{1}}/{{2}}";
    }
}
