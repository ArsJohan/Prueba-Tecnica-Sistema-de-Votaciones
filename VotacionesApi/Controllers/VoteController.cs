using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotacionesApi.DTOs;
using VotacionesApi.Models;
using VotacionesApi.Services;

namespace VotacionesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly IVoteServices _voteService;

        public VoteController(IVoteServices voteService)
        {
            _voteService = voteService;
        }

        /// <summary>
        /// Registra un voto entre un votante y un candidato.
        /// </summary>
        /// <param name="vote">Objeto que contiene los IDs del votante y candidato.</param>
        /// <returns>El voto creado.</returns>
        /// <response code="201">Voto creado exitosamente.</response>
        /// <response code="400">Datos inválidos o fallo en la creación.</response>
        [HttpPost("votes")]
        [ProducesResponseType(typeof(Vote), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Vote>> CreateVote([FromBody] Vote vote)
        {
            try
            {
                if (vote == null)
                    return BadRequest("Información del voto nula");

                var createdVote = await _voteService.RegisterVoteAsync(vote.VoterId, vote.CandidateId);
                if (createdVote == null)
                    return BadRequest("No se pudo registrar el voto");

                return StatusCode(201, createdVote);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creando un voto: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene el total de votos por candidato.
        /// </summary>
        /// <returns>Lista con la cantidad de votos por candidato.</returns>
        /// <response code="200">Datos encontrados exitosamente.</response>
        /// <response code="404">No se encontraron votos.</response>
        /// <response code="400">Error durante la consulta.</response>
        [HttpGet("votes")]
        [ProducesResponseType(typeof(IEnumerable<CandidateStats>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CandidateStats>>> GetVotes()
        {
            try
            {
                var votes = await _voteService.GetVoteCountPerCandidateAsync();
                if (votes == null || !votes.Any())
                    return NotFound("No se encontraron votos");

                return Ok(votes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error obteniendo los votos: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene estadísticas de la votación.
        /// </summary>
        /// <returns>Totales por candidato, porcentajes y votantes que han votado.</returns>
        /// <response code="200">Estadísticas generadas correctamente.</response>
        /// <response code="500">Error del servidor al generar estadísticas.</response>
        [HttpGet("votes/statistics")]
        [ProducesResponseType(typeof(VoteStatisticsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                var stats = await _voteService.GetVoteStatisticsAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener estadísticas: {ex.Message}");
            }
        }
    }
}