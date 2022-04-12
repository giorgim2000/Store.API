using System.ComponentModel.DataAnnotations;

namespace Store.API.Models
{
    public class BaseDto
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string BaseName { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Adress { get; set; } = string.Empty;
        public string CityName { get; set; } = String.Empty;
    }
}
