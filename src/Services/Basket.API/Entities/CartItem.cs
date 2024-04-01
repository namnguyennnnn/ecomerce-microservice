using System.ComponentModel.DataAnnotations;

namespace Basket.API.Entities
{
    public class CartItem
    {
        [Required]
        [Range(1, double.PositiveInfinity, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.1, double.PositiveInfinity, ErrorMessage = "Quantity must be at least 1")]
        public decimal ItemPrice { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public string ItemNo { get; set; }
    }
}
