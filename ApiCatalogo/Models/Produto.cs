using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using APICatalogo.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Models;

[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }
    [StringLength(80, ErrorMessage = "O Nome tem que ter no máximo 80 caracteres e no minimo 3", MinimumLength = 3)]
    [Required(ErrorMessage = "Informe um nome")]
    public string? Nome { get; set; }
    [StringLength(300, ErrorMessage = "A Descricao tem que ter no máximo 300 caracteres e no minimo 3", MinimumLength = 3)]
    [Required(ErrorMessage = "Informe uma Descricao")]
    public string? Descricao { get; set; }
    [Required(ErrorMessage = "Informe um Preço")]
    [Column(TypeName ="decimal(10,2)")]
    public decimal Preco { get; set; }
    [StringLength(80, ErrorMessage = "A URL da Imagem tem que ter no máximo 300 caracteres")]
    [Required(ErrorMessage = "Informe um Imagem URL")]
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }
    [JsonIgnore]
    public Categoria? Categoria { get; set; }
}


public static class ProdutoEndpoints
{
	public static void MapProdutoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Produto").WithTags(nameof(Produto));

        group.MapGet("/", async (AppDbContext db) =>
        {
            return await db.Produtos.ToListAsync();
        })
        .WithName("GetAllProdutos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Produto>, NotFound>> (int produtoid, AppDbContext db) =>
        {
            return await db.Produtos.AsNoTracking()
                .FirstOrDefaultAsync(model => model.ProdutoId == produtoid)
                is Produto model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetProdutoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int produtoid, Produto produto, AppDbContext db) =>
        {
            var affected = await db.Produtos
                .Where(model => model.ProdutoId == produtoid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.ProdutoId, produto.ProdutoId)
                  .SetProperty(m => m.Nome, produto.Nome)
                  .SetProperty(m => m.Descricao, produto.Descricao)
                  .SetProperty(m => m.Preco, produto.Preco)
                  .SetProperty(m => m.ImagemUrl, produto.ImagemUrl)
                  .SetProperty(m => m.Estoque, produto.Estoque)
                  .SetProperty(m => m.DataCadastro, produto.DataCadastro)
                  .SetProperty(m => m.CategoriaId, produto.CategoriaId)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateProduto")
        .WithOpenApi();

        group.MapPost("/", async (Produto produto, AppDbContext db) =>
        {
            db.Produtos.Add(produto);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Produto/{produto.ProdutoId}",produto);
        })
        .WithName("CreateProduto")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int produtoid, AppDbContext db) =>
        {
            var affected = await db.Produtos
                .Where(model => model.ProdutoId == produtoid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteProduto")
        .WithOpenApi();
    }
}