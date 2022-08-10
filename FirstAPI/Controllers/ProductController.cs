using FirstAPI.Data;
using FirstAPI.Dtos.ProductDtos;
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

            ProductReturnDto productReturnDto = new ProductReturnDto();
            productReturnDto.Name = p.Name;
            productReturnDto.Price = p.Price;
            productReturnDto.IsActive = p.IsActive;

            

            return Ok(productReturnDto);
        }
        #endregion

        #region Get All

        // ------- Get All Method --------


        [HttpGet]
        //[Route("all")]
        public IActionResult GetAll()
        {
            var query = _context.Products.Where(p => !p.IsDeleted);

            ProductListDto productListDto = new ProductListDto();

            //foreach (var item in products)
            //{
            //    ProductReturnDto productReturnDto = new ProductReturnDto();
            //    productReturnDto.Name = item.Name;
            //    productReturnDto.Price = item.Price;
            //    productReturnDto.IsActive = item.IsActive;
            //    productListDto.Items.Add(productReturnDto);
            //}

            productListDto.Items = query.Select(p=>new ProductReturnDto
            {
                Name = p.Name,
                Price = p.Price,
                IsActive = p.IsActive,
            }).Skip(1).Take(1).ToList();
                
            productListDto.TotalCount = query.Count();
            return StatusCode(200,productListDto);
        }
        #endregion

        #region Create

        // ------- Create Method --------


        [HttpPost]
        public IActionResult Create(ProductCreateDto productCreateDto)
        {
            Product newProduct = new Product
            {
                Name = productCreateDto.Name,
                Price = productCreateDto.Price,
                IsActive = productCreateDto.IsActive
            };

            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return StatusCode(201);
        }
        #endregion

        #region Update

        // ------- Update Method --------


        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductUpdateDto productUpdateDto)
        {
            Product p = _context.Products.FirstOrDefault(p => p.Id == id);
            if (p == null) return NotFound();

            p.Name = productUpdateDto.Name;
            p.Price = productUpdateDto.Price;
            p.IsActive = productUpdateDto.IsActive;
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
