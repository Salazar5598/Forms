using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using AcmeForms.Models;
using Microsoft.AspNetCore.Authorization;


namespace AcmeForms.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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

        //Create
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] User objeto)
        {
            try
            {
                _dbcontext.Users.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message });
            }
        }

        //Edit
        [HttpPut]
        [Route("edit")]
        public IActionResult Edit([FromBody] User objeto)
        {
            User user = _dbcontext.Users.Find(objeto.UserId);
            if (user == null)
            {
                return BadRequest("Usuario no encontrado");
            }
            try
            {
                user.FullName = objeto.FullName is null ? user.FullName : objeto.FullName;
                user.User1 = objeto.User1 is null ? user.User1 : objeto.User1;
                user.Password = objeto.Password is null ? user.Password : objeto.Password;

                _dbcontext.Users.Update(user);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message });
            }
        }

        //Delete
        [HttpDelete]
        [Route("delete/{idUser:int}")]
        public IActionResult DeleteForm(int idUser)
        {
            User user = _dbcontext.Users.Find(idUser);
            if (user == null)
            {
                return BadRequest("Usuario no encontrado");
            }
            try
            {

                _dbcontext.Users.Remove(user);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message });
            }
        }


    }
}
