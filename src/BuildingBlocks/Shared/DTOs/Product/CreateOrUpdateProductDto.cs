using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Product
{
    public abstract class CreateOrUpdateProductDto
    {
        [Required]
        [MaxLength(250, ErrorMessage = "Maximum lenghh is 250 character.")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Maximum lenghh is 250 character.")]
        public string Summary { get; set; }
     
        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
