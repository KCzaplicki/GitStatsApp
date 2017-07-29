namespace GitStatsApp.Dtos
{
    public class ContibutorStatsDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RepositoryId { get; set; }
        public int Merges { get; set; }
        public int Commits { get; set; }
        public int LinesOfCode { get; set; }
        public double ContribToProject { get; set; }
    }
}
