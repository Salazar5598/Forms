using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using AcmeForms.Models;


namespace AcmeForms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        public readonly acmeformsContext _dbcontext;

        public FormController(acmeformsContext _context)
        {
            _dbcontext = _context;
        }
        //CRUD Form
        //List
        [HttpGet]
        [Route("list")]
        public IActionResult ListForm()
        {
            List<Form> forms = new List<Form>();

            try
            {
                forms = _dbcontext.Forms.Include(c => c.oUser).ToList();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = forms });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = forms });
            }
        }

        //Show
        [HttpGet]
        [Route("show/{idForm:int}")]
        public IActionResult showForm(int idForm)
        {
            Form form = _dbcontext.Forms.Find(idForm);
            if (form == null)
            {
                return BadRequest("Formulario no encotrado");
            }

            try
            {
                form = _dbcontext.Forms.Include(c => c.oUser).Where(p => p.FormId == idForm).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = form });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message, response = form });
            }
        }

        //Create
        [HttpPost]
        [Route("create")]
        public IActionResult CreateForm([FromBody] Form objeto)
        {
            try
            {
                _dbcontext.Forms.Add(objeto);
                _dbcontext.SaveChanges();
                
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message});
            }
        }

        //Edit
        [HttpPut]
        [Route("edit")]
        public IActionResult EditForm([FromBody] Form objeto)
        {
            Form form = _dbcontext.Forms.Find(objeto.FormId);
            if (form == null)
            {
                return BadRequest("Formulario no encontrado");
            }
            try
            {
                form.Name = objeto.Name is null ? form.Name : objeto.Name;
                form.Description = objeto.Description is null ? form.Description : objeto.Description;
                form.Link = objeto.Link is null ? form.Link : objeto.Link;      
                form.UserId = objeto.UserId is null ? form.UserId : objeto.UserId;
              

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message });
            }
        }


        //CRUD Field
        //List
        [HttpGet]
        [Route("field/list")]
        public IActionResult FieldList()
        {
            List<Field> fields = new List<Field>();

            try
            {
                fields = _dbcontext.Fields.Include(c => c.oType).Include(c => c.oForm).ToList();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = fields });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = fields });
            }
        }

        //Show
        [HttpGet]
        [Route("field/show/{idField:int}")]
        public IActionResult showField(int idField)
        {
            Field field = _dbcontext.Fields.Find(idField);
            if (field == null)
            {
                return BadRequest("Campo no encotrado");
            }

            try
            {
                field = _dbcontext.Fields.Include(c => c.oType).Include(c => c.oForm).Where(p => p.FormId == idField).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = field });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message, response = field });
            }
        }





        //CRUD FieldTypes
        //List
        [HttpGet]
        [Route("field/type/list")]
        public IActionResult FieldsTypesList()
        {
            List<FieldsType> fieldtypes = new List<FieldsType>();

            try
            {
                fieldtypes = _dbcontext.FieldsTypes.ToList();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = fieldtypes });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = fieldtypes });
            }
        }

        //Show
        [HttpGet]
        [Route("field/type/show/{idFieldtype:int}")]
        public IActionResult showFieldType(int idFieldtype)
        {
            FieldsType fieldtype = _dbcontext.FieldsTypes.Find(idFieldtype);
            if (fieldtype == null)
            {
                return BadRequest("Campo no encotrado");
            }

            try
            {
                fieldtype = _dbcontext.FieldsTypes.Where(p => p.TypeId == idFieldtype).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = fieldtype });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message, response = fieldtype });
            }
        }


    }
}
