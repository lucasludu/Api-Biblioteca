using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class EditorialCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
