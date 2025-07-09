using EJERCICIOAPI.Data;
using EJERCICIOAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EJERCICIOAPI.Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using EJEMPLOAPI.Models.DTOs;


namespace EJERCICIOAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class EspectaculoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EspectaculoController(AppDbContext context)
        {
            _context = context;
        }

           [HttpGet]
        public ActionResult<List<EspectaculoDTO>> GetEspectaculos()
        {
            List<Espectaculo> espectaculos = _context.Espectaculos
                .Include(e => e.Artista)
                .ToList();

            List<EspectaculoDTO> respuestaEspectaculos = new List<EspectaculoDTO>();

            foreach (var espectaculo in espectaculos)
            {
                var dto = new EspectaculoDTO
                {
                    Id = espectaculo.Id,
                    Titulo = espectaculo.Titulo,
                    Fechayhora = espectaculo.Fechayhora,
                    ArtistaId = espectaculo.ArtistaId,
                    ArtistaNombre = espectaculo.Artista?.Nombre ?? string.Empty
                };

                respuestaEspectaculos.Add(dto);
            }

            return respuestaEspectaculos;
        }


        [HttpGet("{id}")]

        public ActionResult<Espectaculo> GetEspectaculo(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id no puede ser menor o igual a 0");
            }

            var espectaculo = _context.Espectaculos.Include(e => e.Artista).FirstOrDefault(c => c.Id == id); ;
            if (espectaculo == null)
                return NotFound($"Espectaculo con ID {id} no fue encontrado ");

            return Ok(espectaculo);



        }

        [HttpPost]
        public ActionResult<Espectaculo> PostEspectaculo([FromBody] EspectaculoDTO parametrosEspectaculo)
        {
            if (parametrosEspectaculo == null)
                return BadRequest("El cuerpo  del request esta vacío");

            if (string.IsNullOrWhiteSpace(parametrosEspectaculo.Titulo))
                return BadRequest("El nombre del espectáculo es obligatorio.");

            var espectaculoExistenste = _context.Espectaculos.FirstOrDefault(c => c.Titulo == parametrosEspectaculo.Titulo);
            if (espectaculoExistenste != null)
                return BadRequest("Ya existe un espectaculo con ese nombre");

            try
            {
                Espectaculo nuevoEspectaculo = new Espectaculo
                {
                    Titulo = parametrosEspectaculo.Titulo,
                    Fechayhora = parametrosEspectaculo.Fechayhora,
                    ArtistaId = parametrosEspectaculo.ArtistaId
                };

                _context.Espectaculos.Add(nuevoEspectaculo);
                _context.SaveChanges();

                return (nuevoEspectaculo);
            }

            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);


            }
        }
    }
}
 /*       [HttpPut("{id}")]
        public ActionResult<Espectaculo> PutEspectaculo(int id, [FromBody] Espectaculo parametrosEspectaculo)
        {
            if (parametrosEspectaculo == null)
                return BadRequest("El cuerpo del request estaba vacío");

            if (id <= 0 || id != parametrosEspectaculo.Id)
                return BadRequest("Id invalido o no coincide  con el ID de la categoria");

            if (Espectaculo=)

        }
    }
}
*/