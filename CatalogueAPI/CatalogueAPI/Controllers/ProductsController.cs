using CatalogueAPI.Context;
using CatalogueAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogueAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CatalogueDbContext _context;

        public ProductsController(CatalogueDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _context.Products.AsNoTracking().Take(10).ToList();
            if (products is null)
            {
                return NotFound("Products not found.");
            }
            return products;
        }

        [HttpGet("{id:int}", Name="GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            return product;
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return new CreatedAtRouteResult("GetProduct", new {id = product.ProductId}, product);
        }

        [HttpPut("{productId:int}")]
        public ActionResult Put(int productId, Product product)
        {
            if (productId != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpDelete("{productId:int}")]
        public ActionResult Delete(int productId) 
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product is null)
            {
                return NotFound("Product not found.");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok(product);
        }
    }
}
