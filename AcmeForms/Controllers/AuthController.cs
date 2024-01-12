using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using AcmeForms.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using System;

using System.Text;


namespace AcmeForms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly acmeformsContext _dbcontext;

        private readonly string secretkey;
        public AuthController(IConfiguration config, acmeformsContext _context)
        {
            _dbcontext = _context;
            secretkey = config.GetSection("settings").GetSection("secretkey").ToString();
        }

        [HttpPost]
        [Route("authentication")]

        public async Task<IActionResult> auth([FromBody] User request)
        {
            try
            {
                // Buscar el usuario en la base de datos por correo
                var usuarioEnBaseDeDatos = await _dbcontext.Users.FirstOrDefaultAsync(u => u.User1 == request.User1);

                if (usuarioEnBaseDeDatos != null && usuarioEnBaseDeDatos.Password == request.Password)
                {
                    var keyBytes = Encoding.ASCII.GetBytes(secretkey);
                    var claims = new ClaimsIdentity();
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuarioEnBaseDeDatos.User1));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                    string tokencreado = tokenHandler.WriteToken(tokenConfig);

                    return StatusCode(StatusCodes.Status200OK, new { token = tokencreado });
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new { token = "No Authenticated" });
                }
            }
            catch (Exception ex)
            {
                // Maneja cualquier error que pueda ocurrir durante la autenticación
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
