using CatalogueAPI.Context;
using CatalogueAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogueAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        //product
        private readonly CatalogueDbContext _context;

        public CategoriesController(CatalogueDbContext context)
        {
            _context = context;
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
        {
            var categories = _context.Categories.Include(p => p.Products).Where(c => c.CategoryId <= 5).ToList();
            return categories;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _context.Categories.AsNoTracking().Take(10).ToList();
            if (categories is null)
            {
                return NotFound("Categories not found.");
            }
            return categories;
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> Get(int id)
        {
            var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }
            return category;
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            return new CreatedAtRouteResult("GetCategory", new { id = category.CategoryId }, category);
        }

        [HttpPut("{categoryId:int}")]
        public ActionResult Put(int categoryId, Category category)
        {
            if (categoryId != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(category);
        }

        [HttpDelete("{categoryId:int}")]
        public ActionResult Delete(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(p => p.CategoryId == categoryId);
            if (category is null)
            {
                return NotFound("Category not found.");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok(category);
        }
    }
}
