using System.ComponentModel.DataAnnotations;

namespace Application.Endpoint.DTOs
{
    public class CreateDishDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

    public class UpdateDishDto
    {
        public string Name { get; set; }
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
