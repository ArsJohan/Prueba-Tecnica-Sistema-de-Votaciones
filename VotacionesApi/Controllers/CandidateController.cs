using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotacionesApi.Models;
using VotacionesApi.Services;

namespace VotacionesApi.Controllers
{
    public class CandidateController : Controller
    {
        private readonly ICandidateServices _candidateService;

        public CandidateController(ICandidateServices candidateService)
        {
            _candidateService = candidateService;
        }

        // POST: /Candidates
        [HttpPost("Candidates")]
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


        // GET: /Candidates
        [HttpGet("Candidates")]
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

        // GET: /Candidates/{id}
        [HttpGet("Candidates/{id}")]
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

        //DELETE: /Candidates/{id}
        [HttpDelete("Candidates/{id}")]
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
