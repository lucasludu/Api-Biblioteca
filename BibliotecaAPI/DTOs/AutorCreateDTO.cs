using BibliotecaAPI.ValidationAttributes;

namespace BibliotecaAPI.DTOs
{
    public class AutorCreateDTO
    {
        public string Name { get; set; }
        [ExtensionArchivo(TipoArchivoEnum.Image)]
        [PesoArchivo(1024)]
        public IFormFile Image { get; set; }
    }
}
