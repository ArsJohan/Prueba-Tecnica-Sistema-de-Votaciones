using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotacionesApi.Models;
using VotacionesApi.Services;

namespace VotacionesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoterController : ControllerBase
    {
        private readonly IVoterServices _voterService;

        public VoterController(IVoterServices voterService)
        {
            _voterService = voterService;
        }

        /// <summary>
        /// Registra un nuevo votante.
        /// </summary>
        /// <param name="voter">Objeto votante a registrar.</param>
        /// <returns>El votante creado.</returns>
        /// <response code="201">Votante creado exitosamente.</response>
        /// <response code="400">Datos inválidos o error al crear.</response>
        [HttpPost("voters")]
        [ProducesResponseType(typeof(Voter), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Voter>> CreateVoter([FromBody] Voter voter)
        {
            try
            {
                if (voter == null)
                    return BadRequest("Información del votante nula");

                var createdVoter = await _voterService.CreateVoterAsync(voter);
                return CreatedAtAction(nameof(GetVoterById), new { id = createdVoter.Id }, createdVoter);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creando un votante: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los votantes.
        /// </summary>
        /// <returns>Lista de votantes registrados.</returns>
        /// <response code="200">Lista devuelta exitosamente.</response>
        /// <response code="400">Error al obtener los datos.</response>
        [HttpGet("voters")]
        [ProducesResponseType(typeof(IEnumerable<Voter>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Voter>>> GetAllVoters()
        {
            try
            {
                var voters = await _voterService.GetAllVotersAsync();
                return Ok(voters);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al extraer la información de los votantes: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un votante por su ID.
        /// </summary>
        /// <param name="id">ID del votante.</param>
        /// <returns>Objeto votante.</returns>
        /// <response code="200">Votante encontrado.</response>
        /// <response code="404">Votante no encontrado.</response>
        /// <response code="400">Error al procesar la solicitud.</response>
        [HttpGet("voters/{id}")]
        [ProducesResponseType(typeof(Voter), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Voter>> GetVoterById(int id)
        {
            try
            {
                var voter = await _voterService.GetVoterByIdAsync(id);
                if (voter == null)
                    return NotFound($"Votante con ID {id} no encontrado.");

                return Ok(voter);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener el votante: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un votante por su ID.
        /// </summary>
        /// <param name="id">ID del votante.</param>
        /// <returns>Sin contenido si se elimina correctamente.</returns>
        /// <response code="204">Votante eliminado.</response>
        /// <response code="404">Votante no encontrado.</response>
        /// <response code="400">Error al eliminar.</response>
        [HttpDelete("voters/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVoter(int id)
        {
            try
            {
                var deleted = await _voterService.DeleteVoterAsync(id);
                if (!deleted)
                    return NotFound($"Votante con ID {id} no encontrado.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el votante: {ex.Message}");
            }
        }
    }
}
