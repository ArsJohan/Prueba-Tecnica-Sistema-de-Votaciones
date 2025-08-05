using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotacionesApi.Models;
using VotacionesApi.Services;

namespace VotacionesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateServices _candidateService;

        public CandidateController(ICandidateServices candidateService)
        {
            _candidateService = candidateService;
        }

        /// <summary>
        /// Crea un nuevo candidato.
        /// </summary>
        /// <param name="candidate">Objeto del candidato a crear.</param>
        /// <returns>El candidato creado.</returns>
        /// <response code="201">Candidato creado correctamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="500">Error interno al crear el candidato.</response>
        [HttpPost("candidates")]
        [ProducesResponseType(typeof(Candidate), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] Candidate candidate)
        {
            try
            {
                if (candidate == null)
                    return BadRequest("Datos del candidato inválidos.");

                var created = await _candidateService.CreateCandidateAsync(candidate);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear candidato: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los candidatos.
        /// </summary>
        /// <returns>Lista de candidatos.</returns>
        /// <response code="200">Candidatos obtenidos correctamente.</response>
        /// <response code="500">Error interno al obtener candidatos.</response>
        [HttpGet("candidates")]
        [ProducesResponseType(typeof(IEnumerable<Candidate>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var candidates = await _candidateService.GetAllCandidatesAsync();
                return Ok(candidates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener candidatos: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un candidato por ID.
        /// </summary>
        /// <param name="id">ID del candidato.</param>
        /// <returns>El candidato correspondiente.</returns>
        /// <response code="200">Candidato encontrado.</response>
        /// <response code="404">Candidato no encontrado.</response>
        /// <response code="500">Error interno al obtener candidato.</response>
        [HttpGet("candidates/{id}")]
        [ProducesResponseType(typeof(Candidate), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var candidate = await _candidateService.GetCandidateByIdAsync(id);
                if (candidate == null)
                    return NotFound($"Candidato con ID {id} no encontrado.");

                return Ok(candidate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener candidato: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un candidato por ID.
        /// </summary>
        /// <param name="id">ID del candidato a eliminar.</param>
        /// <returns>Sin contenido si se elimina correctamente.</returns>
        /// <response code="204">Candidato eliminado.</response>
        /// <response code="404">Candidato no encontrado.</response>
        /// <response code="500">Error interno al eliminar candidato.</response>
        [HttpDelete("candidates/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _candidateService.DeleteCandidateAsync(id);
                if (!deleted)
                    return NotFound($"Candidato con ID {id} no encontrado.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar candidato: {ex.Message}");
            }
        }
    }
}
