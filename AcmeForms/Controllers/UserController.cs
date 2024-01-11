using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using AcmeForms.Models;


namespace AcmeForms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly acmeformsContext _dbcontext;

        public UserController(acmeformsContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            List<User> users = new List<User>();

            try
            {
                users = _dbcontext.Users.ToList();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = users });

            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = users });
            }
        }

        [HttpGet]
        [Route("show/{idUser:int}")]
        public IActionResult show(int idUser)
        {
            User user = _dbcontext.Users.Find(idUser);
            if (user == null)
            {
                return BadRequest("Usuario no encotrado");
            }

            try
            {
                user = _dbcontext.Users.Where(p => p.UserId == idUser).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = user });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message, response = user });
            }
        }
    }
}
