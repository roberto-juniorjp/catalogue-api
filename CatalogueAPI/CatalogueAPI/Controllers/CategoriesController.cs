﻿using CatalogueAPI.Context;
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

        private readonly CatalogueDbContext _context;

        public CategoriesController(CatalogueDbContext context)
        {
            _context = context;
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
        {
            try
            {
                var categories = _context.Categories.Include(p => p.Products).Where(c => c.CategoryId <= 5).ToList();
                return categories != null ? categories : NotFound("Products not found.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request.");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                var categories = _context.Categories.AsNoTracking().Take(10).ToList();
                return categories is not null ? categories : NotFound("Categories not found.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request.");
            }
        }

        [HttpGet("{categoryId:int}", Name = "GetCategory")]
        public ActionResult<Category> Get(int categoryId)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(p => p.CategoryId == categoryId);
                return category is not null ? category : NotFound("Category with id {categoryId} not found.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request.");
            }
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            try
            {
                if (category is null) return BadRequest("There was a problem.");

                _context.Categories.Add(category);
                _context.SaveChanges();
                return new CreatedAtRouteResult("GetCategory", new { categoryId = category.CategoryId }, category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request.");
            }
        }

        [HttpPut("{categoryId:int}")]
        public ActionResult Put(int categoryId, Category category)
        {
            try
            {
                if (categoryId != category.CategoryId) return BadRequest("There was a problem updating category with id {categoryId}.");

                _context.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request.");
            }
        }

        [HttpDelete("{categoryId:int}")]
        public ActionResult Delete(int categoryId)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(p => p.CategoryId == categoryId);
                if (category is null) return NotFound("Category with id {categoryId} not found.");

                _context.Categories.Remove(category);
                _context.SaveChanges();
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request.");
            }
        }
    }
}
