using FirstAPI.Data;
using FirstAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }


        #region Read

        // ------- Read Method --------


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            Product p = _context.Products.Where(p => p.IsActive).FirstOrDefault(p => p.Id == id);
            if (p == null) return NotFound();

            return Ok(p);
        }
        #endregion

        #region Get All
        // ------- Get All Method --------




        [HttpGet]
        //[Route("all")]
        public IActionResult GetAll()
        {
            return StatusCode(200, _context.Products.Where(p => p.IsActive).ToList());
        }
        #endregion

        #region Create

        // ------- Create Method --------


        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return StatusCode(201);
        }
        #endregion

        #region Update

        // ------- Update Method --------


        [HttpPut("{id}")]
        public IActionResult Update(int id,Product product)
        {
            Product p = _context.Products.FirstOrDefault(p => p.Id == id);
            if (p == null) return NotFound();

            p.Name = product.Name;
            p.Price = product.Price;
            p.IsActive = product.IsActive;
            _context.SaveChanges();

            return StatusCode(200, p.Id);
        }
        #endregion

        #region Change Status

        // ------- Change Status Method --------


        [HttpPatch("{id}")]
        public IActionResult ChangeIsActive(int id, bool isActive)
        {
            Product p = _context.Products.FirstOrDefault(p => p.Id == id);
            if (p == null) return NotFound();

            p.IsActive = isActive;
            _context.SaveChanges();

            return StatusCode(200, p.Id);
        }
        #endregion

        #region Delete

        // ------- Delete Method --------


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product p = _context.Products.FirstOrDefault(p => p.Id == id);
            if (p == null) return NotFound();

            _context.Products.Remove(p);
            _context.SaveChanges();

            return StatusCode(202, p.Id);
        }
        #endregion



    }
}
