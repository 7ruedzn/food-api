using catalogo_api.Context;
using catalogo_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace catalogo_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly CatalogoContext _catalogoContext;
        public ProdutosController(CatalogoContext catalogoContext)
        {
            _catalogoContext = catalogoContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAsync()
        {
            var produtos = await _catalogoContext.Produtos?.AsNoTracking().ToListAsync();
            return produtos is null ? NotFound() : produtos;
        }

        [HttpGet("{id:int}", Name="ObterProduto")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _catalogoContext.Produtos?.AsNoTracking().FirstOrDefaultAsync(p => p.ProdutoId == id);
            return produto is null ? NotFound() : produto;
        }

        [HttpPost]
        public IActionResult Post(Produto produto)
        {   
            if(produto is null) return BadRequest();

            _catalogoContext.Add<Produto>(produto);
            _catalogoContext.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", 
            new { id = produto.ProdutoId}, produto);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Produto produto)
        {
            if(produto is null || id != produto.ProdutoId) return BadRequest();
            _catalogoContext.Entry(produto).State = EntityState.Modified;
            _catalogoContext.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var produto = _catalogoContext.Produtos?.FirstOrDefault(p => p.ProdutoId == id);

            if(produto is null) return NotFound();

            _catalogoContext.Produtos?.Remove(produto);
            _catalogoContext.SaveChanges();
            
            return Ok(produto);
        }
    }
}