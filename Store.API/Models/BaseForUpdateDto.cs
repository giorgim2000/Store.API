using System.ComponentModel.DataAnnotations;

namespace Store.API.Models
{
    public class BaseForUpdateDto
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string BaseName { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Adress { get; set; } = string.Empty;
    }
}
