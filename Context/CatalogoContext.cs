using catalogo_api.Models;
using Microsoft.EntityFrameworkCore;

namespace catalogo_api.Context
{
    public class CatalogoContext : DbContext
    {
        public DbSet<Produto>? Produtos { get; set; }
        public DbSet<Categoria>? Categorias { get; set; }
        public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options)
        {
        }
    }
}