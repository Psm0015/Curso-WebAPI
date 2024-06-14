using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.ToList();

            if(produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }
            else
            {
                return produtos;
            }
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não encontrados");
            }
            else
            {
                return produto;
            }
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if(produto is null)
            {
                return BadRequest("Produto esta vazio");
            }
            _context.Add(produto);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto", new {id = produto.ProdutoId}, produto);
        }

        [HttpPut]
        public ActionResult Put(Produto produto)
        {
            if (produto is null)
            {
                return BadRequest("Produto esta vazio");
            }
            _context.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if(produto is null)
            {
                return NotFound("Produto não Encontrado");
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok();

        }
    }
}
