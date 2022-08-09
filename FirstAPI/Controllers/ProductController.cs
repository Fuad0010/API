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
        public static List<Product> products =
            new List<Product>
            {
                new Product { Id = 1, Name = "Tozsoran", Price = 999.99 },
              { new Product { Id = 2, Name = "Fen", Price = 24.99 } }

            };

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            Product p=products.FirstOrDefault(p=>p.Id==id);
            if(p==null) return NotFound();

            return Ok(p);
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll ()
        {
            return StatusCode(200, products.ToList());
        }




    }
}
