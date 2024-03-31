using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Customer
{
    public class CreateCustomerDto : CreateOrUpdateCustomerDto
    {
        [Required]
        public string UserName { get; set; }
        
    }
}
