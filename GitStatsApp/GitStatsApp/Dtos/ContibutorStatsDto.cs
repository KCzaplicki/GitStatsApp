namespace GitStatsApp.Dtos
{
    public class ContibutorStatsDto
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int Branches { get; set; }
        public int Commits { get; set; }
        public int LinesOfCode { get; set; }
        public double Contribution { get; set; }
    }
}
