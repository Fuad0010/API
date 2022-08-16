using Microsoft.AspNetCore.Http;

namespace FirstAPI.Dtos.CategoryDtos
{
    public class CategoryUpdateDto
    {
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
    }
}
