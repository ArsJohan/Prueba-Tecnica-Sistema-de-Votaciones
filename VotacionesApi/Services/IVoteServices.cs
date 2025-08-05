using VotacionesApi.Models;
using VotacionesApi.DTOs;
namespace VotacionesApi.Services
{
    /// <summary>
    /// Interface para los servicios de votación.
    /// </summary>
    public interface IVoteService
    {
        /// <summary>
        /// Registra un voto de un votante por un candidato.
        /// </summary>
        /// <param name="voterId">ID del votante.</param>
        /// <param name="candidateId">ID del candidato.</param>
        /// <returns>El voto registrado.</returns>
        Task<Vote> RegisterVoteAsync(int? voterId, int? candidateId);

        /// <summary>
        /// Obtiene el total de votos por candidato.
        /// </summary>
        /// <returns>Un diccionario con ID del candidato y total de votos.</returns>
        Task<Dictionary<int, int>> GetVoteCountPerCandidateAsync();

        /// <summary>
        /// Verifica si un votante ya ha votado.
        /// </summary>
        /// <param name="voterId">ID del votante.</param>
        /// <returns>True si ha votado, false en caso contrario.</returns>
        Task<bool> HasVotedAsync(int voterId);


        /// <summary>
        /// Obtiene las estadísticas de votación.
        /// </summary>
        /// <returns>Un objeto con las estadísticas de votación.</returns>
        Task<VoteStatisticsDto> GetVoteStatisticsAsync();
    }
}
