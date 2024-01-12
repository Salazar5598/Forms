using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using AcmeForms.Models;
using Microsoft.VisualBasic.FileIO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient.Server;


namespace AcmeForms.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FormController : ControllerBase
    {

        public readonly acmeformsContext _dbcontext;
        private string _localhostUrl;

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
                forms = _dbcontext.Forms.Include(c => c.oUser).Include(p => p.Fields).ToList();

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
                var httpContext = HttpContext;
                var host = httpContext.Request.Host;
                int numeroAleatorio = new Random().Next(1000, 10000);
                _localhostUrl = $"{httpContext.Request.Scheme}://{host}/{(objeto.Name?.Trim() ?? "").Replace(" ", "-")}/{objeto.UserId}/{numeroAleatorio}";

                var form = new Form
                {
                    Name = objeto.Name,
                    Description = objeto.Description,
                    Link = _localhostUrl,
                    UserId = objeto.UserId,
                };

                _dbcontext.Forms.Add(form);
                _dbcontext.SaveChanges();

                

                return StatusCode(StatusCodes.Status200OK, new { message = "Perfecto! Ahora puedes ver tu formulario en el siguiente enlace: "+_localhostUrl });
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
              
                _dbcontext.Forms.Update(form);
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
        [Route("delete/{idForm:int}")]
        public IActionResult DeleteForm(int idForm)
        {
            Form form = _dbcontext.Forms.Find(idForm);
            if (form == null)
            {
                return BadRequest("Formulario no encontrado");
            }
            try
            {
        
                _dbcontext.Forms.Remove(form);
                _dbcontext.SaveChanges();

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

        //Create
        [HttpPost]
        [Route("field/create")]
        public IActionResult CreateFIeld([FromBody] Field objeto)
        {
            try
            {
                _dbcontext.Fields.Add(objeto);
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
        [Route("field/edit")]
        public IActionResult EditFields([FromBody] Field objeto)
        {
            Field field = _dbcontext.Fields.Find(objeto.FieldId);
            if (field == null)
            {
                return BadRequest("Campo no encontrado");
            }
            try
            {
                field.Name = objeto.Name is null ? field.Name : objeto.Name;
                field.Title = objeto.Title is null ? field.Title : objeto.Title;
                field.Required = objeto.Required is null ? field.Required : objeto.Required;
                field.FormId = objeto.FormId is null ? field.FormId : objeto.FormId;
                field.TypeId = objeto.TypeId is null ? field.TypeId : objeto.TypeId;
                _dbcontext.Fields.Update(field);
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
        [Route("field/delete/{idField:int}")]
        public IActionResult DeleteField(int idField)
        {
            Field field = _dbcontext.Fields.Find(idField);
            if (field == null)
            {
                return BadRequest("Campo no encontrado");
            }
            try
            {

                _dbcontext.Fields.Remove(field);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message });
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
                return BadRequest("Tipo de Campo no encotrado");
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

        //Create
        [HttpPost]
        [Route("field/type/create")]
        public IActionResult CreateFieldType([FromBody] FieldsType objeto)
        {
            try
            {
                _dbcontext.FieldsTypes.Add(objeto);
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
        [Route("field/type/edit")]
        public IActionResult EditFieldType([FromBody] FieldsType objeto)
        {
            FieldsType type = _dbcontext.FieldsTypes.Find(objeto.TypeId);
            if (type == null)
            {
                return BadRequest("Tipo de Campo no encontrado");
            }
            try
            {
                type.Name = objeto.Name is null ? type.Name : objeto.Name;

                _dbcontext.FieldsTypes.Update(type);
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
        [Route("field/type/delete/{idType:int}")]
        public IActionResult DeleteFieldType(int idType)
        {
            FieldsType field = _dbcontext.FieldsTypes.Find(idType);
            if (field == null)
            {
                return BadRequest("Tipo de Campo no encontrado");
            }
            try
            {

                _dbcontext.FieldsTypes.Remove(field);
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
