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
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _catalogoContext.Produtos?.ToList();
            return produtos is null ? NotFound() : produtos;
        }

        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _catalogoContext.Produtos?.Find(id);
            return produto is null ? NotFound() : produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {   
            if(produto is null) return BadRequest();

            _catalogoContext.Add<Produto>(produto);
            _catalogoContext.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", 
            new { id = produto.ProdutoId}, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if(produto is null || id != produto.ProdutoId) return BadRequest();

            // var produtoProcurado = _catalogoContext.Produtos?.Find(produto.ProdutoId);

            // if(produtoProcurado is null) return NotFound();

            // _catalogoContext.Update(produto);
            // _catalogoContext.Attach(produto);
            _catalogoContext.Entry(produto).State = EntityState.Modified;
            _catalogoContext.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            //Pesquisa diretamente no banco;
            var produto = _catalogoContext.Produtos?.FirstOrDefault(p => p.ProdutoId == id);
            
            //Pesquisa primeiro na memória, oq pode gerar erros em alguns momentos;
            // var produto = _catalogoContext.Produtos?.Find(id);

            if(produto is null) return NotFound();

            _catalogoContext.Produtos?.Remove(produto);
            _catalogoContext.SaveChanges();
            
            return Ok(produto);
        }
    }
}