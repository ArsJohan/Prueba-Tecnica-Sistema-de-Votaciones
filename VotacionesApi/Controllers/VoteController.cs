using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotacionesApi.Models;
using VotacionesApi.Services;

namespace VotacionesApi.Controllers
{
    public class VoteController : Controller
    {
        private readonly IVoteService _voteService;
        public VoteController(IVoteService voteService)
        {
            _voteService = voteService ;
        }
        // POST: /votes
        [HttpPost("votes")]
        public ActionResult CreateVote([FromBody] Vote vote)
        {

            try
            {
                if (vote == null)
                {
                    return BadRequest("Información del voto nula");
                }
                var createdVote = _voteService.RegisterVoteAsync(vote.VoterId, vote.CandidateId).Result;
                if (createdVote == null)
                    return BadRequest("No se pudo registrar el voto");
                return StatusCode(201, createdVote); // HTTP 201 Created con el objeto en el cuerpo
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creando un voto: {ex.Message}");
            }
        }

        // GET: /votes
        [HttpGet("votes")] 
        public ActionResult GetVotes()
        {
          
            try
            {
                var votes = _voteService.GetVoteCountPerCandidateAsync().Result;
                if (votes == null || !votes.Any())
                {
                    return NotFound("No se encontraron votos");
                }
                return Ok(votes); // HTTP 200 OK con los votos en el cuerpo
            }
            catch (Exception ex)
            {
                return BadRequest($"Error obteniendo los votos: {ex.Message}");
            }
        }

        // GET: /votes/stadistics
        [HttpGet("votes/stadistics")]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
               
                var stats = await _voteService.GetVoteStatisticsAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                // Devuelve un error 500 con el mensaje de error
                return StatusCode(500, $"Error al obtener estadísticas: {ex.Message}");
            }
        }
    }
}
