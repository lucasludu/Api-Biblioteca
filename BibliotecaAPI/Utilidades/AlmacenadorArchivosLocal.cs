namespace BibliotecaAPI.Utilidades
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AlmacenadorArchivosLocal(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task Borrar(string ruta, string container)
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwrootPath))
            {
                throw new Exception();
            }
            var nombreArchivo = Path.GetFileName(ruta);
            string pathFinal = Path.Combine(wwwrootPath, container, nombreArchivo);

            if (File.Exists(pathFinal))
            {
                File.Delete(pathFinal);
            }
            return Task.CompletedTask;
        }

        public async Task<string> Crear(byte[] file, string contentType, string extension, string container, string nombre)
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;

            if(string.IsNullOrEmpty(wwwrootPath))
            {
                throw new Exception();
            }

            string carpetaArchivo = Path.Combine(wwwrootPath, container);
            
            if(!Directory.Exists(carpetaArchivo))
            {
                Directory.CreateDirectory(carpetaArchivo);
            }

            string nombreFinal = $"{nombre}{extension}";
            string rutaFinal = Path.Combine(carpetaArchivo, nombreFinal);

            await File.WriteAllBytesAsync(rutaFinal, file);

            string urlActual = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            string dbUrl = Path.Combine(urlActual, container, nombreFinal).Replace("\\", "/");

            return dbUrl;
        }
    }
}
