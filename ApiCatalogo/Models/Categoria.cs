using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Categorias")]
public class Categoria
{
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }
    [Key]
    public int CategoriaId { get; set; }
    [StringLength(80, ErrorMessage ="O Nome tem que ter no máximo 80 caracteres e no minimo 3", MinimumLength =3)]
    [Required(ErrorMessage ="Informe um nome")]
    public string? Nome { get; set; }
    [StringLength(80, ErrorMessage = "A URL da Imagem tem que ter no máximo 300 caracteres")]
    [Required(ErrorMessage = "Informe um Imagem URL")]
    public string? ImageUrl { get; set; }
    public ICollection<Produto>? Produtos { get; set; }
}
