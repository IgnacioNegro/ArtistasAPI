using EJERCICIOAPI.Data;
using EJERCICIOAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EJERCICIOAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Categoria
        [HttpGet]
        public ActionResult<List<Categoria>> GetCategorias()
        {
            // Incluyo Artistas relacionados
            return _context.Categorias.Include(c => c.Artistas).ToList();
        }

        // GET: api/Categoria/5
        [HttpGet("{id}")]
        public ActionResult<Categoria> GetCategoria(int id)
        {
            if (id <= 0)
                return BadRequest("Id no puede ser menor o igual a cero");

            var categoria = _context.Categorias.Include(c => c.Artistas).FirstOrDefault(c => c.Id == id);

            if (categoria == null)
                return NotFound($"Categoría con Id ({id}) no fue encontrada");

            return Ok(categoria);
        }

        // POST: api/Categoria
        [HttpPost]
        public ActionResult<Categoria> PostCategoria([FromBody] Categoria parametrosCategoria)
        {
            if (parametrosCategoria == null)
                return BadRequest("El cuerpo del request estaba vacío");

            if (string.IsNullOrWhiteSpace(parametrosCategoria.Nombre))
                return BadRequest("El nombre de la categoría es obligatorio");

            // Verificar si ya existe una categoria con ese nombre
            var categoriaExistente = _context.Categorias.FirstOrDefault(c => c.Nombre == parametrosCategoria.Nombre);
            if (categoriaExistente != null)
                return BadRequest("Ya existe una categoría con ese nombre");

            try
            {
                _context.Categorias.Add(parametrosCategoria);
                _context.SaveChanges();
                return Ok(parametrosCategoria);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Categoria/5
        [HttpPut("{id}")]
        public ActionResult<Categoria> PutCategoria(int id, [FromBody] Categoria parametrosCategoria)
        {
            if (parametrosCategoria == null)
                return BadRequest("El cuerpo del request estaba vacío");

            if (id <= 0 || id != parametrosCategoria.Id)
                return BadRequest("Id inválido o no coincide con el Id de la categoría");

            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                return NotFound("No existe una categoría con ese Id");

            if (string.IsNullOrWhiteSpace(parametrosCategoria.Nombre))
                return BadRequest("El nombre de la categoría es obligatorio");

            var categoriaPorNombre = _context.Categorias.FirstOrDefault(c => c.Nombre == parametrosCategoria.Nombre && c.Id != id);
            if (categoriaPorNombre != null)
                return BadRequest("Ya existe una categoría con ese nombre");

            categoria.Nombre = parametrosCategoria.Nombre;

            try
            {
                _context.Categorias.Update(categoria);
                _context.SaveChanges();
                return Ok(categoria);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Categoria/5
        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteCategoria(int id)
        {
            if (id <= 0)
                return BadRequest("Es necesario un Id válido");

            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);

            if (categoria == null)
                return NotFound("Categoría no encontrada");

            try
            {
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
