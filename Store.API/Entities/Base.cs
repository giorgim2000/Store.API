using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.API.Entities
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string BaseName { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string Adress { get; set; } = string.Empty;
        [Required]
        [ForeignKey("CityId")]
        public int CityId { get; set; }
        public City? City { get; set; }

    }
}
