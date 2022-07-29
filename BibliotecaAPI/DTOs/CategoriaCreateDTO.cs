using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class CategoriaCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
