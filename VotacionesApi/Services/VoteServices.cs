using Microsoft.EntityFrameworkCore;
using VotacionesApi.DTOs;
using VotacionesApi.Models;

namespace VotacionesApi.Services
{
    /// <summary>
    /// Clase de servicios para manejar operaciones relacionadas con votaciones.
    /// </summary>
    public class VoteServices : IVoteService
    {
        // Inyección del contexto de la base de datos.
        private readonly ElectoralContext dbcontext;

        public VoteServices(ElectoralContext context)
        {
            dbcontext = context;
        }


        public async Task<Vote> RegisterVoteAsync(int? voterId, int? candidateId)
        {
            try
            {
                var voter = await (dbcontext.Voters).FindAsync(voterId);
                if (voter == null || (voter.HasVoted ?? false))
                    throw new InvalidOperationException("Votante no válido.");

                var candidate = await dbcontext.Candidates.FindAsync(candidateId);
                if (candidate == null)
                    throw new InvalidOperationException("Candidato no válido.");

                var vote = new Vote
                {
                    VoterId = voterId,
                    CandidateId = candidateId,
                };

                voter.HasVoted = true;

                dbcontext.Votes.Add(vote);
                await dbcontext.SaveChangesAsync();

                return vote;
            }
            catch
            {
                throw new InvalidOperationException("No se pudo registrar el voto.");
            }
        }

        public async Task<Dictionary<int, int>> GetVoteCountPerCandidateAsync()
        {
            return await dbcontext.Votes
                .GroupBy(v => v.CandidateId)
                .Select(g => new { CandidateId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.CandidateId ?? 0, x => x.Count);
        }

        public async Task<bool> HasVotedAsync(int voterId)
        {
            var voter = await dbcontext.Voters.FindAsync(voterId);
            if (voter == null)
                throw new InvalidOperationException("Votante no encontrado.");
            return voter.HasVoted ?? false;

        }

        public async Task<VoteStatisticsDto> GetVoteStatisticsAsync()
        {
            try
            {
                var totalVotes = await dbcontext.Votes.CountAsync();
                var totalVotersWhoVoted = await dbcontext.Voters.CountAsync(v => v.HasVoted == true);

                var candidateStats = await dbcontext.Candidates
                    .Select(c => new CandidateStats
                    {
                        CandidateId = c.Id,
                        CandidateName = c.Name,
                        TotalVotes = dbcontext.Votes.Count(v => v.CandidateId == c.Id),
                        VotePercentage = totalVotes == 0 ? 0 :
                            ((double)dbcontext.Votes.Count(v => v.CandidateId == c.Id) / totalVotes) * 100
                    })
                    .ToListAsync();

                return new VoteStatisticsDto
                {
                    Candidates = candidateStats,
                    TotalVotersWhoVoted = totalVotersWhoVoted
                };
            }
            catch (Exception ex)
            {
          
                throw new Exception("Error al obtener las estadísticas de votos", ex);
            }
        }
    }
}
