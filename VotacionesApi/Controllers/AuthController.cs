using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VotacionesApi.DTOs;

namespace VotacionesApi.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        /// <summary>
        /// Controla el inicio de sesión del usuario y genera un token JWT.
        /// </summary>
        /// <param name="user">Objeto que contiene el nombre de usuario y contraseña.</param
        /// <returns>Retorna un token JWT si las credenciales son válidas.</returns>
        public IActionResult Login([FromBody] UserLogin user)
        {
            //Punto extra - Validación de JWT para  proteger Endpoints
            // Validación simple de usuario y contraseña (esto es solo un ejemplo).
            // En un caso real, se verificarian con una base de datos.
            if (user.Username == "admin" && user.Password == "1234")
            {
                var token = GenerateJwtToken(user.Username);
                return Ok(new { token });
            }

            return Unauthorized("Credenciales inválidas");
        }


        /// <summary>
        /// Genera un token JWT para el usuario autenticado.
        /// </summary>
        /// <param name="username">Nombre de usuario del usuario autenticado.</param>
        /// <returns>Retorna el token generado.</returns>
        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
