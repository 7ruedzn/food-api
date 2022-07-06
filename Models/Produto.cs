using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using catalogo_api.Validations;

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
        [ImageAttribute]
        public string? ImagemURL { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        public float Estoque { get; set; }
        
        public DateTime DataCadastro { get; set; }

        //Referênciando a chave estrangeira de Categoria
        public int CategoriaId { get; set; }
        
        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}