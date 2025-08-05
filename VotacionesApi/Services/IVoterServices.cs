using VotacionesApi.Models;

namespace VotacionesApi.Services
{
    /// <summary>
    /// Interface para los servicios relacionados con votantes.
    /// </summary>
    public interface IVoterService
    {
        /// <summary>
        /// Registra un nuevo votante.
        /// </summary>
        /// <param name="voter">Objeto votante a registrar.</param>
        /// <returns>El votante registrado.</returns>
        Task<Voter> CreateVoterAsync(Voter voter);

        /// <summary>
        /// Obtiene todos los votantes registrados.
        /// </summary>
        /// <returns>Lista de votantes.</returns>
        Task<IEnumerable<Voter>> GetAllVotersAsync();

        /// <summary>
        /// Obtiene un votante por su ID.
        /// </summary>
        /// <param name="id">ID del votante.</param>
        /// <returns>El votante encontrado o null si no existe.</returns>
        Task<Voter> GetVoterByIdAsync(int id);

        /// <summary>
        /// Elimina un votante por su ID.
        /// </summary>
        /// <param name="id">ID del votante a eliminar.</param>
        /// <returns>True si se eliminó correctamente, false si no existía.</returns>
        Task<bool> DeleteVoterAsync(int id);
    }
}
