using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.ValidationAttributes
{
    public class PesoArchivoAttribute : ValidationAttribute
    {
        private readonly double pesoArchivoKb;

        public PesoArchivoAttribute(double pesoArchivoKb)
        {
            this.pesoArchivoKb = pesoArchivoKb;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var formfile = value as IFormFile;
            if(formfile != null)
            {
                if(formfile.Length/1024 > pesoArchivoKb)
                {
                    return new ValidationResult($"El peso maximo para el archivo que envias es de {pesoArchivoKb} KB sin embargo has enviado un archivo con {formfile.Length/1024}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
