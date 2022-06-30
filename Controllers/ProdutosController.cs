using catalogo_api.Context;
using catalogo_api.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}