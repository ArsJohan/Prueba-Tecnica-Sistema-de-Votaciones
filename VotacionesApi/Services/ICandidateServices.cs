using VotacionesApi.Models;

namespace VotacionesApi.Services
{
    /// <summary>
    /// Interface para los servicios relacionados con candidatos.
    /// </summary>
    public interface ICandidateServices
    {
        /// <summary>
        /// Registra un nuevo candidato.
        /// </summary>
        /// <param name="candidate">Objeto candidato a registrar.</param>
        /// <returns>El candidato registrado.</returns>
        Task<Candidate> CreateCandidateAsync(Candidate candidate);

        /// <summary>
        /// Obtiene todos los candidatos registrados.
        /// </summary>
        /// <returns>Lista de candidatos.</returns>
        Task<IEnumerable<Candidate>> GetAllCandidatesAsync();

        /// <summary>
        /// Obtiene un candidato por su ID.
        /// </summary>
        /// <param name="id">ID del candidato.</param>
        /// <returns>El candidato encontrado o null si no existe.</returns>
        Task<Candidate> GetCandidateByIdAsync(int id);

        /// <summary>
        /// Elimina un candidato por su ID.
        /// </summary>
        /// <param name="id">ID del candidato a eliminar.</param>
        /// <returns>True si se eliminó correctamente, false si no existía.</returns>
        Task<bool> DeleteCandidateAsync(int id);
    }
}
