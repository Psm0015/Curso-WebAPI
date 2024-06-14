using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _context.Categorias;

            if(categorias is null)
            {
                return NotFound("Categorias Não encontradas");
            }
            return Ok(categorias);
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            var categorias = _context.Categorias.Include(p => p.Produtos).ToList();

            if(categorias is null)
            {
                return NotFound("Categorias Não encontradas");
            }
            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if(categoria is null)
            {
                return NotFound("Categoria Não Encontrada");
            }
            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult<Categoria> Post(Categoria categoria)
        {
            if(categoria is null)
            {
                return BadRequest("Categoria esta vazio");
            }
            _context.Add(categoria);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId}, categoria);
        }

        [HttpPut]
        public ActionResult Put(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Categoria esta vazio");
            }
            _context.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if(categoria is null)
            {
                return NotFound("Categoria não Encontrada");
            }
            _context.Remove(categoria);
            _context.SaveChanges();
            return Ok();
        }
    }
}
