namespace VotacionesApi.DTOs
{
    public class VoteStatisticsDto
    {
        public List<CandidateStats> Candidates { get; set; } = new();
        public int TotalVotersWhoVoted { get; set; }
    }

    public class CandidateStats
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; } = "";
        public int TotalVotes { get; set; }
        public double VotePercentage { get; set; }
    }
}
