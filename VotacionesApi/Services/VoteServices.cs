using Microsoft.EntityFrameworkCore;
using VotacionesApi.DTOs;
using VotacionesApi.Models;

namespace VotacionesApi.Services
{
    /// <summary>
    /// Clase de servicios para manejar operaciones relacionadas con votaciones.
    /// </summary>
    public class VoteServices : IVoteServices
    {
        // Inyección del contexto de la base de datos.
        private readonly ElectoralContext dbcontext;

        public VoteServices(ElectoralContext context)
        {
            dbcontext = context;
        }


        public async Task<Vote> RegisterVoteAsync(int voterId, int candidateId)
        {
            try
            {
                var voter = await (dbcontext.Voters).FindAsync(voterId);
                if (voter == null || voter.HasVoted)
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

        public async Task<Dictionary<string, int>> GetVoteCountPerCandidateAsync()
        {
            return await dbcontext.Votes
                .Include(v => v.Candidate)
                .Where(v => v.Candidate != null)
                .GroupBy(v => v.Candidate!.Name)
                .Select(g => new
                {
                    CandidateName = g.Key,
                    VoteCount = g.Count()
                })
                .ToDictionaryAsync(x => x.CandidateName, x => x.VoteCount);
        }

        public async Task<bool> HasVotedAsync(int voterId)
        {
            var voter = await dbcontext.Voters.FindAsync(voterId);
            if (voter == null)
                throw new InvalidOperationException("Votante no encontrado.");
            return voter.HasVoted;

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
