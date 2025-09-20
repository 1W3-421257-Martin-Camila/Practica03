using Microsoft.AspNetCore.Mvc;
using ParcialWebApi.Models;
using ParcialWebApi.Repositories;
using ParcialWebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParcialWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly ISolicitudService _service;
        public SolicitudController(ISolicitudService service)
        {
            _service = service;
        }

        //Validaciones
        private bool IsValidSave(Solicitud s)
        {
            return 
                !string.IsNullOrEmpty(s.Estado)
                && !string.IsNullOrEmpty(s.Detalle)
                && s.TipoCambio > 0;
        }
        private bool IsValidUpdate(Solicitud s)
        {
            return 
            !string.IsNullOrEmpty(s.Estado)
            && !string.IsNullOrEmpty(s.Detalle)
            && s.TipoCambio > 0
            && !s.Estado.Equals("Cancelada");
        }

        // GET: api/<SolicitudController>
        [HttpGet]
        public IActionResult GetAll(string tipoCambio)
        {
            try
            {
                var solicitudes = _service.GetAll(tipoCambio);
                if (solicitudes == null)
                {
                    return NotFound("No hay Solicitudes");
                }
                return Ok(solicitudes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }

        // POST api/<SolicitudController>
        [HttpPost]
        public IActionResult Post([FromBody] Solicitud solicitud)
        {
            try
            {
                if (IsValidSave(solicitud))
                {
                    _service.Create(solicitud);
                    return StatusCode(201, "Creado correctamente");
                }
                else
                {
                    return BadRequest("Ingrese todos los datos necesarios");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }

        // PUT api/solicitudes/5?estado=En%20Proceso
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromQuery] string estado)
        {
            try
            {
                // Validación rápida: estado no puede ser vacío ni "Cancelada"
                if (string.IsNullOrEmpty(estado) || estado.Equals("Cancelada"))
                    return BadRequest("Estado inválido");

                // Llama al service que actualiza usando EF
                var actualizado = _service.Update(id, estado);

                if (!actualizado)
                    return BadRequest("No se pudo actualizar la solicitud");

                return Ok("Estado actualizado correctamente");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }

    }
}
