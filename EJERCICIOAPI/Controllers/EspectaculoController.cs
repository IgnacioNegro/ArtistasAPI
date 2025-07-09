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
        public ActionResult<List<EspectaculoRecibirDTO>> GetEspectaculos()
        {
            List<Espectaculo> espectaculos = _context.Espectaculos.Include(e => e.Artista).ToList();

            if (espectaculos == null)
            {
                return NotFound("No se encontraron espectaculos");
            }


            List<EspectaculoRecibirDTO> respuestaEspectaculos = new List<EspectaculoRecibirDTO>();


            foreach (var espectaculo in espectaculos)
            {
                var dto = new EspectaculoRecibirDTO
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
                return BadRequest("Id no puede ser menor o igual a cero");

            Espectaculo? espectaculo = _context.Espectaculos
                .Include(e => e.Artista)
                .FirstOrDefault(e => e.Id == id);

            if (espectaculo == null)
                return NotFound($"Espectáculo con Id ({id}) no fue encontrado");

            EspectaculoRecibirDTO espectaculoDTO = new EspectaculoRecibirDTO
            {
                Id = espectaculo.Id,
                Titulo = espectaculo.Titulo,
                Fechayhora = espectaculo.Fechayhora,
                ArtistaId = espectaculo.ArtistaId
            };

            return Ok(espectaculoDTO);
        }

        [HttpPost]
        public ActionResult<Espectaculo> PostEspectaculo([FromBody] EspectaculoCrearDTO parametrosEspectaculo)
        {
            if (parametrosEspectaculo == null)
                return BadRequest("El cuerpo  del request esta vacío");

            if (string.IsNullOrWhiteSpace(parametrosEspectaculo.Titulo))
                return BadRequest("El nombre del espectáculo es obligatorio.");

            var espectaculoExistenste = _context.Espectaculos.FirstOrDefault(c => c.Titulo == parametrosEspectaculo.Titulo);
            if (espectaculoExistenste != null)
                return BadRequest("Ya existe un espectaculo con ese nombre");

            Artista? artista= _context.Artistas.FirstOrDefault(a => a.Id == parametrosEspectaculo.ArtistaId);
            if (artista == null)
            {
                return BadRequest("El artista no existe o no es válido. Por favor, verifique el ID del artista.");
            }
            Espectaculo nuevoEspectaculo = new Espectaculo
            {
                Titulo = parametrosEspectaculo.Titulo,
                Fechayhora = parametrosEspectaculo.Fechayhora,
                ArtistaId = parametrosEspectaculo.ArtistaId,
                ArtistaNombre = artista.Nombre

            };


            _context.Espectaculos.Add(nuevoEspectaculo);

            try
            {
                _context.SaveChanges();
                parametrosEspectaculo.Id = nuevoEspectaculo.Id;

                return Ok(parametrosEspectaculo);
            }

            catch (System.Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);

            }
        }


        [HttpPut("{id}")]
        public ActionResult<Espectaculo> PutEspectaculo(int id, [FromBody] EspectaculoCrearDTO parametrosEspectaculo)
        {
            if (parametrosEspectaculo == null)
                return BadRequest("El cuerpo del request estaba vacío");
            Espectaculo espectaculo = _context.Espectaculos.FirstOrDefault(c => c.Id == id);
            if (id <= 0 || id != espectaculo.Id)
                return BadRequest("Id invalido o no coincide  con el ID del espectáculo");

          
           
            if (espectaculo == null)
                return NotFound("No existe un espectáculo con ese Id");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            Artista? artista = _context.Artistas.FirstOrDefault(a => a.Id == parametrosEspectaculo.ArtistaId);
            if (artista == null)
            {
                return BadRequest("El artista no existe o no es válido. Por favor, verifique el ID del artista.");
            }
            espectaculo.Titulo = parametrosEspectaculo.Titulo;
            espectaculo.Fechayhora = parametrosEspectaculo.Fechayhora;
            espectaculo.ArtistaId = parametrosEspectaculo.ArtistaId;
            try
            {
                _context.SaveChanges();
                espectaculo.Id= parametrosEspectaculo.Id;
                return Ok(parametrosEspectaculo);

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteEspectaculo(int id)
        {
            if (id <= 0)
                return BadRequest("Id no puede ser menor o igual a cero");

            Espectaculo? espectaculo = _context.Espectaculos.FirstOrDefault(c => c.Id == id);
            if (espectaculo == null)
                return NotFound($"No existe un espectáculo con Id ({id})");

            _context.Espectaculos.Remove(espectaculo);
            try
            {
                _context.SaveChanges();
                return Ok(espectaculo);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    


}