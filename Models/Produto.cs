using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace catalogo_api.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }
        [StringLength(300)]
        public string? Descricao { get; set; }
        [StringLength(300)]
        public string? ImagemURL { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }

        //Referênciando a chave estrangeira de Categoria
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}