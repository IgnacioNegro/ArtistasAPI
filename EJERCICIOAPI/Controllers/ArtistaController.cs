using EJERCICIOAPI.Data;
using EJERCICIOAPI.Models;
using Microsoft.AspNetCore.Mvc;
using EJERCICIOAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using EJEMPLOAPI.Models.DTOs;
using System.Security.Claims;


    namespace EJERCICIOAPI.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public ActionResult<RespuestaArtistaDTO> PostArtista([FromBody] ArtistaDTO parametrosArtista)
        {
            string? usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (usuarioId == null || usuarioId == string.Empty)
                return Unauthorized("No se pudo obtener el Id del usuario autenticado");

            if (parametrosArtista == null)
                return BadRequest("El cuerpo del request estaba vacio");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            Usuario? usuario = _context.Usuarios.FirstOrDefault(u => u.Id.ToString() == usuarioId);

            if (usuario == null)
                return Unauthorized("Usuario no encontrado");

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
                parametrosArtista.CategoriaId,
                usuario.Id
            );

            _context.Artistas.Add(artista);

            try
            {
                _context.SaveChanges();
                RespuestaArtistaDTO respuestaArtista = new RespuestaArtistaDTO();
                respuestaArtista.Id = artista.Id;
                respuestaArtista.Nombre = artista.Nombre;
                respuestaArtista.Genero = artista.Genero ?? string.Empty;
                respuestaArtista.FechaNacimiento = artista.FechaNacimiento.ToString("yyyy-MM-dd");
                respuestaArtista.Nacionalidad = artista.Nacionalidad ?? string.Empty;
                respuestaArtista.CategoriaNombre = categoria.Nombre;
                respuestaArtista.CategoriaId = artista.CategoriaId ?? 0;
                respuestaArtista.UsuarioEmail = usuario.Email;
                respuestaArtista.UsuarioId = usuario.Id;

                return Ok(respuestaArtista);
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
