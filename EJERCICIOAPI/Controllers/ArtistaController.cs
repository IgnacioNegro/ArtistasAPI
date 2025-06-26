using EJERCICIOAPI.Data;
using EJERCICIOAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Artistas.Models.DTOs;
using Microsoft.EntityFrameworkCore;


    namespace Artistas.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ArtistasController : ControllerBase
        {
            private readonly AppDbContext _context;

            public ArtistasController(AppDbContext context)
            {
                _context = context;
            }

            // GET: api/Artistas
            [HttpGet]
            public ActionResult<List<Artista>> GetArtistas()
            {
                return _context.Artistas.ToList();
            }

            // GET: api/Artistas/5
            [HttpGet("{id}")]
            public ActionResult<Artista> GetArtista(int id)
            {
                if (id <= 0)
                    return BadRequest("Id no puede ser menor o igual a cero");

                Artista? artista = _context.Artistas.FirstOrDefault(artista => artista.Id == id);

                if (artista == null)
                    return NotFound($"Artista con Id ({id}) no fue encontrado");

                return Ok(artista);
            }

            // POST: api/Artistas
            [HttpPost]
            public ActionResult<Artista> PostArtista([FromBody] ArtistaDTO parametrosArtista)
            {
                if (parametrosArtista == null)
                    return BadRequest("El cuerpo del request estaba vacio");

                if (ModelState.IsValid == false)
                    return BadRequest(ModelState);

                Categoria? categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == parametrosArtista.CategoriaId);

                if (categoria == null)
                    return BadRequest("La categoria no existe");

                Artista? artista = _context.Artistas.FirstOrDefault(artista => artista.Nombre == parametrosArtista.Nombre);

                if (artista != null)
                    return BadRequest("Ya existe un Artista con ese nombre");

                artista = new Artista(
                    parametrosArtista.Nombre,
                    parametrosArtista.Genero,
                    parametrosArtista.FechaNacimiento,
                    parametrosArtista.Nacionalidad,
                    parametrosArtista.CategoriaId
                );

                _context.Artistas.Add(artista);

                try
                {
                    _context.SaveChanges();
                    return Ok(artista);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            // PUT: api/Artistas/5
            [HttpPut("{id}")]
            public ActionResult<Artista> PutArtista(int id, [FromBody] ArtistaDTO parametrosArtista)
            {
                if (parametrosArtista == null)
                    return BadRequest("El cuerpo del request estaba vacio");

                Artista? artista = _context.Artistas.FirstOrDefault(artista => artista.Id == id);

                if (artista == null)
                    return NotFound("No existe un artista con ese Id");

                if (ModelState.IsValid == false)
                    return BadRequest(ModelState);

                Categoria? categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == parametrosArtista.CategoriaId);

                if (categoria == null)
                    return BadRequest("La categoria no existe");

                Artista? artistaPorNombre = _context.Artistas.FirstOrDefault(artista => artista.Nombre == parametrosArtista.Nombre);

                if (artistaPorNombre != null)
                    return BadRequest("Ya existe un Artista con ese nombre");

                artista.Nombre = parametrosArtista.Nombre;
                artista.Genero = parametrosArtista.Genero;
                artista.FechaNacimiento = parametrosArtista.FechaNacimiento;
                artista.Nacionalidad = parametrosArtista.Nacionalidad;
                artista.CategoriaId = parametrosArtista.CategoriaId;

                try
                {

                    _context.Artistas.Update(artista);
                    _context.SaveChanges();
                    return Ok(artista);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }



            // DELETE: api/Artistas/5
            [HttpDelete("{id}")]
            public ActionResult<bool> DeleteArtista(int id)
            {
                if (id <= 0)
                    return BadRequest("Es necesario un Id");

                Artista? artista = _context.Artistas.FirstOrDefault(artista => artista.Id == id);

                if (artista == null)
                    return NotFound("Artista no encontrado");

                try
                {
                    _context.Artistas.Remove(artista);
                    return Ok(true);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
