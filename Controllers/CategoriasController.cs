using catalogo_api.Context;
using catalogo_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace catalogo_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly CatalogoContext _catalogoContext;

        public CategoriasController(CatalogoContext catalogoContext)
        {
            _catalogoContext = catalogoContext;
        }
        
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _catalogoContext.Categorias?.Find(id);
            return categoria is null ? NotFound() : categoria;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _catalogoContext.Categorias?.ToList();
            return categorias is null ? NotFound() : categorias;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetProdutosPorCategoria(int id)
        {
            var categorias = _catalogoContext.Categorias?.Include(c => c.Produtos).ToList();

            return categorias is null ? NotFound() : categorias;
        }

        [HttpPost]
        public ActionResult<Categoria> Post(Categoria categoria)
        {
            if(categoria is null) return BadRequest();

            _catalogoContext.Categorias?.Add(categoria);
            _catalogoContext.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId, categoria});
        }

        [HttpPut("{id:int}")]
        public ActionResult<Categoria> Put(int id, Categoria categoria)
        {
            if(categoria is null || categoria.CategoriaId != id) return BadRequest();

            _catalogoContext.Entry<Categoria>(categoria).State = EntityState.Modified;
            _catalogoContext.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _catalogoContext.Categorias?.FirstOrDefault(c => c.CategoriaId == id);

            if(categoria is null) return NotFound();

            _catalogoContext.Categorias?.Remove(categoria);
            _catalogoContext.SaveChanges();

            return Ok(categoria);
        }
    }
}