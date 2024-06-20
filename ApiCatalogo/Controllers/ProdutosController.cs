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
            
            try
            {
                var produtos = _context.Produtos.ToList();

                if (produtos is null)
                {
                    return NotFound("Produtos não encontrados");
                }
                else
                {
                    return produtos;
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor. Não foi possivel realizar a solicitação");
            }
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {

            try
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
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor. Não foi possivel realizar a solicitação");
            }
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
           
            try
            {
                if (produto is null)
                {
                    return BadRequest("Produto esta vazio");
                }
                produto.DataCadastro = DateTime.Now;
                _context.Add(produto);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor. Não foi possivel realizar a solicitação");
            }
        }

        [HttpPut]
        public ActionResult Put(Produto produto)
        {
            

            try
            {
                if (produto is null)
                {
                    return BadRequest("Produto esta vazio");
                }
                _context.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

                return Ok(produto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor. Não foi possivel realizar a solicitação");
            }

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {

            try
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

                if (produto is null)
                {
                    return NotFound("Produto não Encontrado");
                }
                _context.Produtos.Remove(produto);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor. Não foi possivel realizar a solicitação");
            }

        }
    }
}
