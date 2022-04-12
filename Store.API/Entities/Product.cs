using System.ComponentModel.DataAnnotations;

namespace Store.API.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string BarCode { get; set; } = string.Empty;
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
