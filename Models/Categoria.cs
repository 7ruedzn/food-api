using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace catalogo_api.Models
{
    public class Categoria
    {
        public Categoria()
        {
            Collection<Produto> Produtos = new ();
        }
        
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImagemURL { get; set; }
        public Collection<Produto>? Produtos { get; set; }
    }
}