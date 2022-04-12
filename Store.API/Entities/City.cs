using System.ComponentModel.DataAnnotations;

namespace Store.API.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string CityName { get; set; } = string.Empty;
        public ICollection<Base> Bases { get; set; } = new List<Base>();
    }
}
