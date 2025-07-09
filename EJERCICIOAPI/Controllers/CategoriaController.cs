using EJERCICIOAPI.Data;
using EJERCICIOAPI.Models;
using EJERCICIOAPI.Models.DTOs;
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
        public ActionResult<List<CategoriaDTO>> GetCategorias()
        {
            var categoriasDTO = _context.Categorias
                .Select(c => new CategoriaDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                })
                .ToList();

            return Ok(categoriasDTO);
        }

        // GET: api/Categoria/5
        [HttpGet("{id}")]
        public ActionResult<CategoriaDTO> GetCategoria(int id)
        {
            if (id <= 0)
                return BadRequest("Id no puede ser menor o igual a cero");

            var categoria = _context.Categorias
                .Where(c => c.Id == id)
                .Select(c => new CategoriaDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                
                })
                .FirstOrDefault();

            if (categoria == null)
                return NotFound($"Categor�a con Id ({id}) no fue encontrada");

            return Ok(categoria);
        }
        // POST: api/Categoria
        [HttpPost]
        public ActionResult<CategoriaDTO> PostCategoria([FromBody] CategoriaPostDTO parametrosCategoria)
        {
            if (parametrosCategoria == null)
                return BadRequest("El cuerpo del request estaba vac�o");

            if (string.IsNullOrWhiteSpace(parametrosCategoria.Nombre))
                return BadRequest("El nombre de la categor�a es obligatorio");

            var categoriaExistente = _context.Categorias.FirstOrDefault(c => c.Nombre == parametrosCategoria.Nombre);
            if (categoriaExistente != null)
                return BadRequest("Ya existe una categor�a con ese nombre");

            var nuevaCategoria = new Categoria
            {
                Nombre = parametrosCategoria.Nombre
            };

            try
            {
                _context.Categorias.Add(nuevaCategoria);
                _context.SaveChanges();

                var resultadoDTO = new CategoriaDTO
                {
                    Id = nuevaCategoria.Id,
                    Nombre = nuevaCategoria.Nombre
                };

                return Ok(resultadoDTO);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT: api/Categoria/5
        [HttpPut("{id}")]
        public ActionResult<CategoriaDTO> PutCategoria(int id, [FromBody] CategoriaPostDTO parametrosCategoria)
        {
            if (parametrosCategoria == null)
                return BadRequest("El cuerpo del request estaba vac�o");

            if (id <= 0)
                return BadRequest("Id inv�lido");

            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                return NotFound("No existe una categor�a con ese Id");

            if (string.IsNullOrWhiteSpace(parametrosCategoria.Nombre))
                return BadRequest("El nombre de la categor�a es obligatorio");

            var categoriaPorNombre = _context.Categorias.FirstOrDefault(c => c.Nombre == parametrosCategoria.Nombre && c.Id != id);
            if (categoriaPorNombre != null)
                return BadRequest("Ya existe una categor�a con ese nombre");

            categoria.Nombre = parametrosCategoria.Nombre;

            try
            {
                _context.Categorias.Update(categoria);
                _context.SaveChanges();

                var resultadoDTO = new CategoriaDTO
                {
                    Id = categoria.Id,
                    Nombre = categoria.Nombre
                };

                return Ok(resultadoDTO);
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
                return BadRequest("Es necesario un Id v�lido");

            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);

            if (categoria == null)
                return NotFound("Categor�a no encontrada");

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
