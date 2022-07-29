using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.ValidationAttributes
{
    public class ExtensionArchivoAttribute : ValidationAttribute
    {
        private readonly string[] tiposValidos;

        public ExtensionArchivoAttribute(string[] tiposValidos)
        {
            this.tiposValidos = tiposValidos;
        }

        public ExtensionArchivoAttribute(TipoArchivoEnum tipoArchivo)
        {
            if(tipoArchivo ==  TipoArchivoEnum.Image)
            {
                tiposValidos = new[] { "image/png", "image/jpg", "image/gif" };
            }
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var formfile = value as IFormFile;
            if(formfile != null)
            {
                if(!tiposValidos.Contains(formfile.ContentType))
                {
                    return new ValidationResult($"Los tipos validos son {string.Join(",", tiposValidos)}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
