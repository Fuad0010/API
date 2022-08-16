using FirstAPI.Data;
using FirstAPI.Dtos.CategoryDtos;
using FirstAPI.Extetions;
using FirstAPI.Helper;
using FirstAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FirstAPI.Controllers
{

    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        #region Read

        // ------- Read Method --------


        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Category c = _context.Categories.FirstOrDefault(p => p.Id == id && !p.IsDeleted);

            if (c == null) return NotFound();
            CategoryReturnDto categoryReturnDto = new CategoryReturnDto();
            categoryReturnDto.Name = c.Name;
            categoryReturnDto.ImageUrl = "http://localhost:61263/img/"+c.ImageUrl;

            return Ok(categoryReturnDto);
        }
        #endregion

        #region Get All

        // ------- Get All Method --------


        [HttpGet]
        //[Route("all")]
        public IActionResult GetAll()
        {
            var query = _context.Categories.Where(p => !p.IsDeleted);

            CategoryListDto categoryListDto = new CategoryListDto();
            categoryListDto.Items = query.Select(c => new CategoryReturnDto
            {
                Name = c.Name,
                ImageUrl = "http://localhost:61263/img/" + c.ImageUrl
            }).ToList();

            categoryListDto.TotalCount = query.Count();
            return StatusCode(200, categoryListDto);
        }
        #endregion

        [HttpPost]
        public IActionResult Create([FromForm]CategoryCreateDto categoryCreateDto)
        {
            bool existCategory = _context.Categories.Any(c => c.Name.ToLower() == categoryCreateDto.Name.ToLower());
            
            if (existCategory) return StatusCode(409);

            if (!categoryCreateDto.Photo.IsImage()) return BadRequest();
            if (!categoryCreateDto.Photo.ValidSize(200)) return BadRequest();

            Category newCategory = new Category();
            newCategory.Name = categoryCreateDto.Name;
            newCategory.CreateTime = DateTime.Now;
            newCategory.UpdateTime = DateTime.Now;
            newCategory.ImageUrl = categoryCreateDto.Photo.SaveImage(_env,"img");
            _context.Add(newCategory);
            _context.SaveChanges();

            return NoContent();
        }

        #region Update

        // ------- Update Method --------


        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryUpdateDto categoryUpdateDto)
        {
            Category c = _context.Categories.FirstOrDefault(p => p.Id == id);
            if(c == null) return NotFound();

            if (_context.Categories.Any(c => c.Name.ToLower() == categoryUpdateDto.Name.ToLower() && c.Id != id))
            {
                return BadRequest();
            }

            if (categoryUpdateDto.Photo != null)
            {

                // -------- 31:00 ---------//

                Helper.Helper.DeleteImage("");
            }
            return Ok("");
        }
        #endregion



    }
}
