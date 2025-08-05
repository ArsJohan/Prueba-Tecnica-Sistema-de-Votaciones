using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotacionesApi.Models;
using VotacionesApi.Services;

namespace VotacionesApi.Controllers
{
    public class VoterController : Controller
    {
        private readonly IVoterService _voterService;
        public VoterController(IVoterService voterService)
        {
            _voterService = voterService;
        }
        // POST: /voters
        [HttpPost("voters")]
        public ActionResult CreateVoter([FromBody] Voter voter)
        {
           
            try
            {
                if (voter == null)
                {
                    return BadRequest("Información del votante nula");
                }
                var createdVoter = _voterService.CreateVoterAsync(voter).Result;
                return CreatedAtAction(nameof(GetAllVoters), new { id = createdVoter.Id }, createdVoter);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creando un votante: {ex.Message}");
            }
        }


        // GET: /voters
        [HttpGet("voters")]
        public ActionResult GetAllVoters()
        {
            try
            {
                var voters = _voterService.GetAllVotersAsync().Result;
                return Ok(voters);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al extraer la informacion de los votantes: {ex.Message}");
            }
        }

        // GET: /voters/{id}
        [HttpGet("voters/{id}")]
        public ActionResult GetVoterById(int id)
        {
           
            try
            {
                var voter = _voterService.GetVoterByIdAsync(id).Result;
                if (voter == null)
                {
                    return NotFound($"Votante con ID {id} no encontrado.");
                }
                return Ok(voter);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener el votante: {ex.Message}");
            }
        }

        // DELETE: /voters/{id}
        [HttpDelete("voters/{id}")]
        public ActionResult DeleteVoter(int id)
        {
            try
            {
                var deleted = _voterService.DeleteVoterAsync(id).Result;
                if (!deleted)
                {
                    return NotFound($"Votante con ID {id} no encontrado.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el votante: {ex.Message}");
            }

        }
    }
}
