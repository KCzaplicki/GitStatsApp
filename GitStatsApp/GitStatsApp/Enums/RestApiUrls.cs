namespace GitStatsApp.Enums
{
    public class RestApiUrls
    {
        public static readonly string ApiUrl = "http://192.168.1.104:3000/api";

        public static readonly string GetRepositoriesUrl = $"{ApiUrl}/repository";
        public static readonly string GetRepositoryContributorsUrl = $"{ApiUrl}/repository/{{0}}";
        public static readonly string GetContributorStatsUrl = $"{ApiUrl}/contributor/{{0}}";
        public static readonly string GetContributorStatsWithRangeUrl = $"{ApiUrl}/contributor/{{0}}/{{1}}/{{2}}";
    }
}
