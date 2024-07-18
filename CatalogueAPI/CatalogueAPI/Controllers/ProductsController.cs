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
            try
            {
                var products = _context.Products.AsNoTracking().Take(10).ToList();
                return products != null ? products : NotFound("Products not found.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request.");
            }
        }

        [HttpGet("{id:int}", Name="GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
                return product != null ? product : NotFound("Product not found.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request.");
            }
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            try
            {
                if (product == null) return BadRequest();
                
                _context.Products.Add(product);
                _context.SaveChanges();
                return new CreatedAtRouteResult("GetProduct", new { id = product.ProductId }, product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request.");
            }
        }

        [HttpPut("{productId:int}")]
        public ActionResult Put(int productId, Product product)
        {
            try
            {
                if (productId != product.ProductId) return BadRequest();

                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request.");
            }
        }

        [HttpDelete("{productId:int}")]
        public ActionResult Delete(int productId) 
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);

                if (product is null) return NotFound("Product not found.");

                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request.");
            }
        }
    }
}
